#light
/// Code Kata 1: http://codingdojo.org/cgi-bin/wiki.pl?KataBankOCR
namespace KataBankOCR

open System
open System.IO

exception DigitException of string

type Checksum() =
  member cs.validate(account:string)=
   let rec checksumValue (pos:int,result:int):int =
     if pos < 0 then
       result
     else
       let result = result + Convert.ToInt32(Char.ToString(account.[pos])) * (account.Length - pos)
       let pos = pos - 1 
       checksumValue(pos, result)
   let mutable rem = 0
   let csv = checksumValue(account.Length - 1,0)
   let quotient = Math.DivRem(csv,11,&rem)
   if rem <> 0 then
    false
   else
    true
   
type Entry() =
  let mutable hasErrors = false
  let parseDigit(f,s,t) =
    match (f,s,t) with
    |(" _ ",
      "| |",
      "|_|") -> '0'
    |("   ",
      " | ",
      " | ") -> '1'
    |(" _ ",
      " _|",
      "|_ ") -> '2'
    |(" _ ",
      " _|",
      " _|") -> '3'
    |("   ",
      "|_|",
      "  |") -> '4'
    |(" _ ",
      "|_ ",
      " _|") -> '5'
    |(" _ ",
      "|_ ",
      "|_|") -> '6'
    |(" _ ",
      "  |",
      "  |") -> '7'
    |(" _ ",
      "|_|",
      "|_|") -> '8'
    |(" _ ",
      "|_|",
      " _|") -> '9'
    |_-> 
      hasErrors <- true
      '?'
  let rec parseEntry (f:string, s:string, t:string, current:int, e:string) =
    if current > 26 then
      if hasErrors then
        e + " ILL"
      else
        //Is the only way in F#? ... seems quiet imperative :-S
        let checksum = new Checksum()
        if checksum.validate(e) then
          e
        else
          e + " ERR" 
    else
      let fd = f.[current .. current+2] 
      let sd = s.[current .. current+2] 
      let td = t.[current .. current+2]
      let entry = String.Concat(e, parseDigit(fd, sd, td))
      parseEntry(f,s,t,current + 3, entry) 
  member e.parse(f,s,t) = 
      parseEntry(f, s, t, 0, "")

type File() =
  let rec parseLine(streamReader:StreamReader, result:string) =
   let s1 = streamReader.ReadLine()
   if s1 <> null then
     let  s2 = streamReader.ReadLine()
     let  s3 = streamReader.ReadLine()
     let  s4 = streamReader.ReadLine()
     let entry = new Entry()
     parseLine(streamReader, result + entry.parse(s1,s2,s3) + "\n")
   else
     result

  member f.parse(fileName:string) = 
    let streamReader =  File.OpenText(fileName)
    parseLine(streamReader,"")
