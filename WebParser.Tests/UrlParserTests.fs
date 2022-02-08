module WebParser.Tests

open NUnit.Framework
open WebParser

[<SetUp>]
let Setup () =
    ()

let runTest expectedList functionToTest = 
    let errorMessage expected actual = sprintf "Expected %A but was %A" expected actual
    
    let setupList = [
        "https://www.google.com/search?q=Sanja"
        "https://yandex.ru/home"
        "maps.yahoo.com/Riga"
        "megatel.lv/lietotajs"
    ]
    let actualList = setupList |> List.map(fun x -> functionToTest x)

    Assert.AreEqual(expectedList, actualList, errorMessage expectedList actualList)

[<Test>]
let ``Get scheme`` () = runTest [Some("https"); Some("https"); None; None] UrlParser.getSheme

[<Test>]
let ``Get subdomain`` () = runTest [Some("www"); None; Some("maps"); None] UrlParser.getSubdomain

[<Test>]
let ``Get host name `` () = runTest ["google.com"; "yandex.ru"; "yahoo.com"; "megatel.lv"] UrlParser.getHostname