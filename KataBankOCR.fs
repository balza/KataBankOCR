#light

namespace KataBanlOCR

open System

type File() =
  let enties = Array.zeroCreate 500
  member f.Parse() = "a"

[<Struct>]
type Entry =
  val FirstRow : string
  val SecondRow : string
  val ThirdRow : string
  val FourthRow : string
  new (first,second,third)={ FirstRow = first; SecondRow = second; ThirdRow = third; FourthRow = "" }
  new (first,second,third,fourth)={ FirstRow = first; SecondRow = second; ThirdRow = third; FourthRow = fourth }
  member e.Parse()=123456789 


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
