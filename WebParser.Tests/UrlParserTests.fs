module WebParser.Tests

open NUnit.Framework
open WebParser

[<SetUp>]
let Setup () =
    ()

let runTest functionToTest expectedList = 
    let errorMessage expected actual = sprintf "Expected %A but was %A" expected actual
    
    let setupList = [
        "https://www.google.com/search?q=Sanja"
        "https://yandex.ru/home"
        "maps.yahoo.com/Riga"
        "megatel.lv/lietotajs"
        "/posts/123/"
    ]
    let actualList = setupList |> List.map(fun x -> functionToTest x)

    Assert.AreEqual(expectedList, actualList, errorMessage expectedList actualList)

[<Test>]
let ``Get scheme`` () = 
    runTest UrlParser.getSheme [Some("https"); Some("https"); None; None; None] 

[<Test>]
let ``Get subdomain`` () = 
    runTest UrlParser.getSubdomain [Some("www"); None; Some("maps"); None; None]

[<Test>]
let ``Get host name`` () = 
    runTest UrlParser.getHostname [Some("google.com"); Some("yandex.ru"); Some("yahoo.com"); Some("megatel.lv"); None]

[<Test>]
let ``Is absolute url`` () = 
    runTest UrlParser.isAbsoluteUrl [true; true; true; true; false]