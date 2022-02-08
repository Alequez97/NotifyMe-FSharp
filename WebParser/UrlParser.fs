namespace HtmlParser

open System
open System.Net.Http

module UrlParser =
    let isAbsoluteUrl(url: string) = url.Contains("http")
    
    let getUrlSheme(url: string) = 
        match isAbsoluteUrl url with
        | true -> 
            url.Split([|"://"|], StringSplitOptions.TrimEntries).[0]
        | false -> ""

    let getSubdomain(url: string) =
        let countChars(str: string, c: char) = str |> Seq.filter (fun x -> x = c) |> Seq.length
        
        match isAbsoluteUrl url with
        | true ->
            let urlWithoutSchema = url.Split([|"://"|], StringSplitOptions.TrimEntries).[1]
            if countChars(urlWithoutSchema, '.') >= 2 then urlWithoutSchema.Split('.').[0] else ""
        | false -> ""

    let getUrlHost url =
        match isAbsoluteUrl url with
        | true ->
            let subdomain = getSubdomain url
            let urlWithoutSchema = url.Split([|"://"|], StringSplitOptions.TrimEntries).[1]
            urlWithoutSchema.Replace(subdomain, "").Split('/').[0]
        | false -> ""