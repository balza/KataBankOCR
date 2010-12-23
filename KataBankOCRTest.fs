#light
namespace KataBanlOCR

open System
open NUnit.Framework

[<TestFixture>]
type FileTest() = 
  let mutable file = File()

  /// This is not necessary since the mutable testableClass is already set
  /// but is just for showing how to define a test fixture setup method.
  [<TestFixtureSetUp>]
  member ft.TestFixtureSetUp () = file <- File()

  [<Test>]
  member ft.ParseTest () =  Assert.AreEqual("a", file.Parse())

[<TestFixture>]
type EntryTest() =
  [<Test>]
  member et.ParseTest123456789() = 
    //let entry = new Entry("  _  _     _  _  _  _  _ ","| _| _||_||_ |_   ||_||_|","||_  _|  | _||_|  ||_| _|")
    let entry = new Entry()
    Assert.AreEqual("123456789",entry.parse("    _  _     _  _  _  _  _ "," |  _| _||_||_ |_   ||_||_|"," | |_  _|  | _||_|  ||_| _|"))
  
  [<Test>]
  member et.ParseTest023456789()= 
    //let entry = new Entry(" _  _  _     _  _  _  _  _ ","| | _| _||_||_ |_   ||_||_|","|_||_  _|  | _||_|  ||_| _|")
    let entry = new Entry()
    Assert.AreEqual("023456789",entry.parse(" _  _  _     _  _  _  _  _ ","| | _| _||_||_ |_   ||_||_|","|_||_  _|  | _||_|  ||_| _|"))

[<TestFixture>]
type DigitTest()=
  [<Test>]
  member dt.parse0()=
    let digit = new Digit()
    Assert.AreEqual(0,digit.parse(" _ ","| |","|_|"))

  [<Test>]
  member dt.parse1()=
    let digit = new Digit()
    Assert.AreEqual(1,digit.parse("   "," | "," | "))

  [<Test>]
  member dt.parse2()=
    let digit = new Digit()
    Assert.AreEqual(2,digit.parse(" _ "," _|","|_ "))

  [<Test>]
  member dt.parse3()=
    let digit = new Digit()
    Assert.AreEqual(3,digit.parse(" _ "," _|"," _|"))

  [<Test>]
  member dt.parse4()=
    let digit = new Digit()
    Assert.AreEqual(4,digit.parse("   ","|_|","  |"))

  [<Test>]
  member dt.parse5()=
    let digit = new Digit()
    Assert.AreEqual(5,digit.parse(" _ ","|_ "," _|"))

  [<Test>]
  member dt.parse6()=
    let digit = new Digit()
    Assert.AreEqual(6,digit.parse(" _ ","|_ ","|_|"))

  [<Test>]
  member dt.parse7()=
    let digit = new Digit()
    Assert.AreEqual(7,digit.parse(" _ ","  |","  |"))

  [<Test>]
  member dt.parse8()=
    let digit = new Digit()
    Assert.AreEqual(8,digit.parse(" _ ","|_|","|_|"))

  [<Test>]
  member dt.parse9()=
    let digit = new Digit()
    Assert.AreEqual(9,digit.parse(" _ ","|_|"," _|"))
