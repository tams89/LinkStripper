open a

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    LinkStripper.saveLinksToFile
    0