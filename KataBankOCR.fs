#light

namespace KataBanlOCR

open System

type File() =
  let enties = Array.zeroCreate 500
  member f.Parse() = "a"

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


type Entry() =
  let rec parseDigit (f:string, s:string, t:string, current:int, e:string) =
    if current > 26 then
      e
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
      let digit = new Digit()
      let entry = String.Concat(e, digit.parse(fd.ToString(),sd.ToString(),td.ToString()))
      parseDigit(f,s,t,current + 3, entry)
  member e.parse(f,s,t) = parseDigit (f,s,t,0,"")
