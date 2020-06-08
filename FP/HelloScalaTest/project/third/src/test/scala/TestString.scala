package exercise3
import org.scalatest.FunSuite
class TestString extends FunSuite{
  test("testUppercase"){
    val p = Strings.testUppercase("wfie")
    assert(p == "WаIE")
  }
  test("testInterpolations"){
    val p = Strings.testInterpolations("Vova",12)
    assert(p ==  "Hi my name is Vova and I am 12 years old.")
  }
  test("testComputation"){
    val p = Strings.testComputation(4,5)
    assert(p == "Hi,\nnow follows a quite hard calculation. We try to add:\n\ta := 4\n\tb := 5\n\n\tresult is 9")
  }
  test("testTakeTwo: 2"){
    val p = Strings.testTaleTwo("KO")
    assert(p == "KO")
  }
  test("testTakeTwo: 5"){
    val p = Strings.testTaleTwo("Hello")
    assert(p == "He")
  }
}
