namespace a

module LinkStripper = 
    open System.Net
    open System.Text.RegularExpressions
    open System.IO
    open System

    /// Extracts links from HTML
    /// Usage getHtml "http://...."
    let getHtml url = 
        let wb = new WebClient()
        let html = wb.DownloadString(url : string)
        let parseLinks = Regex.Matches(html, "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))")
        [ for m in parseLinks -> m.Value.Replace("href=", "") ]

    // Example execution
    let saveLinksToFile =
        let links = new System.Collections.Generic.List<string>()
        let path sp = Environment.GetFolderPath(sp)
        let linksFile = path Environment.SpecialFolder.Desktop + "\\Links.txt"
        if File.Exists(linksFile) then
            let readLinks  = File.ReadAllLines(linksFile);
            if readLinks.Length > 0 then
                for l in readLinks do 
                    links.Add(l)

        for link in links do
            let strippedLinks = getHtml link
            let path = path Environment.SpecialFolder.Desktop + "\\ExtractedLinks.txt"
            File.WriteAllLines(path, (getHtml link))
            printfn "Links extracted from %A and saved to %A" link path
