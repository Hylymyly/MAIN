package exercise3
import org.scalatest.FunSuite
class TestSequence extends FunSuite{
  test("testLastElement 5 2 6 1 ")
  {
    val p = Sequence.testLastElement(Seq(5,2,6,1))
    assert(p == Some(1))
  }
  test("testZip: 1 2 3 and 3 2 1")
  {
    val p = Sequence.testZip(Seq(1,2,3),Seq(3,2,1))
    assert(p == Seq((1,3),(2,2),(3,1)))
  }
  test("testForAll: 2,4,6")
  {
    val cond = (i: Int) => i>2
    val p = Sequence.testForAll(Seq(2,4,6))(cond)
    assert(p == false)
  }
  test("testForAll: 2,4,6,8")
  {
    val cond = (i: Int) => i>1
    val p = Sequence.testForAll(Seq(2,4,6,8))(cond)
    assert(p == true)
  }
  test("testPalindrom: 2,4,4,2")
  {
    val p = Sequence.testPalindrom(Seq(2,4,4,2))
    assert(p == true)
  }
  test("testFlatMap: ")
  {
    val A = (i: Seq[Int])=> i
    val p = Sequence.testFlatMap(Seq(Seq(2,3),Seq(4,5)))(A)
    assert(p == Seq(4,5,2,3))
  }
}
