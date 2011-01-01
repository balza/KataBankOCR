#light
namespace KataBankOCR

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
  ///Test the file testfile.txt with 1 account
  member ft.ParseTest1 () = 
    printfn "ParseTest1" 
    let fileName =  @"testfile.txt"
    Assert.AreEqual("123456789\n", file.parse(fileName))

  [<Test>]
  ///Test the file testfile2.txt with 2 accounts
  member ft.ParseTest2 () =
    printfn "ft.ParseTest2"
    let fileName =  @"testfile2.txt"
    Assert.AreEqual("123456789\n123456789\n", file.parse(fileName))

[<TestFixture>]
type EntryTest() =
  [<Test>]
  member et.ParseTest000000000() =
    printfn "ParseTest000000000"
    let entry = new Entry()
    Assert.AreEqual("000000000",entry.parse(" _  _  _  _  _  _  _  _  _ ",
                                            "| || || || || || || || || |",
                                            "|_||_||_||_||_||_||_||_||_|"))
  [<Test>]
  member et.ParseTest123456789() = 
    let entry = new Entry()
    Assert.AreEqual("123456789",entry.parse("    _  _     _  _  _  _  _ ",
                                            " |  _| _||_||_ |_   ||_||_|",
                                            " | |_  _|  | _||_|  ||_| _|"))
  
  [<Test>]
  member et.ParseTest023456789ChecksumFail()= 
    let entry = new Entry()
    Assert.AreEqual("023456789 ERR",entry.parse(" _  _  _     _  _  _  _  _ ",
                                                "| | _| _||_||_ |_   ||_||_|",
                                                "|_||_  _|  | _||_|  ||_| _|"))

  [<Test>]
  member et.ParseTest02345678IllegalDigit()=
    printfn "ParseTest02345678IllegalDigit"
    let entry = new Entry()
    Assert.AreEqual("02345678? ILL",entry.parse(" _  _  _     _  _  _  _  _ ",
                                                "| | _| _||_||_ |_   ||_||_|",
                                                "|_||_  _|  | _||_|  ||_|  |"))
[<TestFixture>]
  type ChecksumTest()=
   [<Test>]
   member cst.parseTestOK()=
     let checksum = new Checksum()
     Assert.IsTrue(checksum.validate("457508000"))

   [<Test>]
   member cst.parseTestFail()=
     let checksum = new Checksum()
     Assert.IsFalse(checksum.validate("123456788"))
