﻿namespace WebParser

open System
open System.Net.Http

module UrlParser =
    let isAbsoluteUrl(url: string) = url.Contains("http")
    
    let getSheme(url: string): string option = 
        match url.Contains("://") with
        | true -> Some(url.Split([|"://"|], StringSplitOptions.TrimEntries).[0])
        | false -> None

    let getSubdomain(url: string): string option =
        let countChars(str: string, c: char) = str |> Seq.filter (fun x -> x = c) |> Seq.length
        let getSubdomainHelper url = if countChars(url, '.') >= 2 then Some(url.Split('.').[0]) else None
        
        match url.Contains("://") with
        | true ->
            let urlWithoutSchema = url.Split([|"://"|], StringSplitOptions.TrimEntries).[1]
            getSubdomainHelper urlWithoutSchema
        | false -> getSubdomainHelper url

    let getHostname(url: string) =
        let subdomain = getSubdomain url
        let removeSubdomain(url: string): string= 
            match subdomain with
            | Some(x) -> url.Replace(x + ".", "")
            | None -> url

        match url.Contains("://") with
        | true ->
            let urlWithoutSchema = url.Split([|"://"|], StringSplitOptions.TrimEntries).[1]
            (removeSubdomain urlWithoutSchema).Split('/').[0]
        | false ->
            (removeSubdomain url).Split('/').[0]