package Lab_ex4
import org.scalatest.FunSuite
class TestTypeclass extends FunSuite {

  test("testReversableString"){
    val p = Typeclasses.textReversableString("kvak")
    assert(p == "kavk")
  }
  test("testSmashInt"){
    val p = Typeclasses.testSmashInt(6,3)
    assert(p == 9)
  }
  test("testSmashDouble"){
    val p = Typeclasses.testSmashDouble(3.0,3.0)
    assert(p == 9.0)
  }
  test("testSmashString"){
    val p = Typeclasses.testSmashString("He", "llo")
    assert(p == "Hello")
  }
}
