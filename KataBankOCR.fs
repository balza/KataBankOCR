#light
/// Code Kata 1: http://codingdojo.org/cgi-bin/wiki.pl?KataBankOCR
namespace KataBanlOCR

open System
open System.IO

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
    |_-> 
      printfn "%s" f
      printfn "%s" s
      printfn "%s" t
      raise(System.Exception("bad stuff happened"))


type Entry() =
  let rec parseDigit (f:string, s:string, t:string, current:int, e:string) =
    if current > 26 then
      e
    else
      let fd = f.[current .. current+2] 
      let sd = s.[current .. current+2] 
      let td = t.[current .. current+2]
      printfn "%s %i %i" fd current (current+2)
      printfn "%s" sd
      printfn "%s" td 
      let digit = new Digit()
      let entry = String.Concat(e, digit.parse(fd, sd, td))
      parseDigit(f,s,t,current + 3, entry)
  member e.parse(f,s,t) = 
      parseDigit (f, s, t, 0, "")

type File() =
  let rec parseLine(streamReader:StreamReader, result:string) =
   let s1 = streamReader.ReadLine()
   if s1 <> null then
     let  s2 = streamReader.ReadLine()
     let  s3 = streamReader.ReadLine()
     let  s4 = streamReader.ReadLine()
     let entry = new Entry()
     printfn "%i" s1.Length
     printfn "%i" s2.Length
     printfn "%i" s3.Length
     parseLine(streamReader, result + entry.parse(s1,s2,s3) + "\n")
   else
     result

  member f.parse(fileName:string) = 
    let streamReader =  File.OpenText(fileName)
    parseLine(streamReader,"")
