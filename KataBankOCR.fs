#light

namespace KataBanlOCR

open System

type File() =
  let enties = Array.zeroCreate 500
  member f.Parse() = "a"

type Entry() =
  let rec parseDigit (f:string,s:string,t:string,current:int) =
    if current > 27 then
      printfn "then %d" current 
    else
      let fd = new System.Text.StringBuilder()
      fd.Append(f.[current])
      fd.Append(f.[current+1])
      fd.Append(f.[current+2])
      let sd = new System.Text.StringBuilder()
      sd.Append(s.[current])
      sd.Append(s.[current+1])
      sd.Append(s.[current+2])
      let td = new System.Text.StringBuilder()
      td.Append(t.[current])
      td.Append(t.[current+1])
      td.Append(t.[current+2])
      printfn "%s" (fd.ToString())
      printfn "%s" (sd.ToString())
      printfn "%s" (td.ToString())


//        Digit digit = new Digit()
//        let entry = String.Concat(parseDigit (f,s,t, (current + 3)), digit.parse(frd,srd,trd))
      parseDigit(f,s,t,current + 3)
  member e.parse(f,s,t) = parseDigit (f,s,t,0)

type Digit()=
  member d.parse(f,s,t) =
    match (f,s,t) with
    |(" _ ","| |","|_|") -> 0 
    |("   "," | "," | ") -> 1
    |(" _ "," _|","|_ ") -> 2 
    |(" _ "," _|"," _|") -> 3
    |("   ","|_|","  |") -> 4
    |(" _ ","|_ "," _|") -> 5
    |(" _ ","|_ ","|_|") -> 6
    |(" _ ","  |","  |") -> 7
    |(" _ ","|_|","|_|") -> 8
    |(" _ ","|_|"," _|") -> 9
    |_-> raise(System.Exception("bad stuff happened"))
