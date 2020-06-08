package exercise2
import org.scalatest.FunSuite
class TestCompositions extends FunSuite{
  val sw:(Double)=>Int = _.toInt
  val sw1:(Int)=>Byte = _.toByte
  val sw2:(Byte)=>String = _.toString
  test("testCompose: 1134 to String ")
  {
    val p = Compositions.testCompose(sw)(sw1)(sw2)
    assert(p(1134) == "110")
  }
  test("testMapFlatMap: double -> int -> byte -> string"){
    val A1: (Double) => Int = _.toInt
    val B1: (Int) => Byte = _.toByte
    val C1: (Byte) => String = _.toString

    val A: (Double) => Option[Int] = Some(_).map(A1)
    val B: (Int) => Option[Byte] = Some(_).map(B1)
    val C: (Byte) => String = _.toString
    val p = Compositions.testMapFlatMap(A)(B)(C)
    assert(p(Some(1134)).toString == (Some(110).toString))
  }


  //test testForComprehension
  test("testForComprehension: double -> int -> byte -> string"){
    val A1: (Double) => Int = _.toInt
    val B1: (Int) => Byte = _.toByte

    val A: (Double) => Option[Int] = Some(_).map(A1)
    val B: (Int) => Option[Byte] = Some(_).map(B1)
    val C: (Byte) => String = _.toString
    val p = Compositions.testMapFlatMap(A)(B)(C)
    assert(p(Some(1134)).toString == (Some(110).toString))
  }
}
