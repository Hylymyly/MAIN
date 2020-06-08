package exercise3


import scala.util.{Try,Failure,Success}
object Adts{

  def GetNth(list: List[Int],n: Int): Option[Int] = list match {
    case head :: tail => Option(list(n))
    case Nil => null
    case l => Option(l(n))
  }

  def testGetNth(list: List[Int],n:Int):Option[Int]= GetNth(list,n)

  def DoubleD(n: Option[Int]): Option[Int] = n match {
    case Some(a) => Option(a*2)
    case None =>null
  }

  def testDouble(n: Option[Int]): Option[Int]= DoubleD(n)

  def IsEven(n: Int): Either[String,Int] = Either.cond(n%2==0,n,"Нечетное число") match{
    case Left(i) => Left(i)
    case Right(s)=> Right(s)
  }

  def testIsEven(n: Int): Either[String,Int] = IsEven(n)

  def SafeJava(a: Int, b:Int): Either[String,Int] = Try(a/b) match{
    case Success(a) => Right(a)
    case Failure(error) =>Left("Вы не можете делить на ноль.")
  }

  def testSafeDivide(a:Int, b:Int): Either[String,Int] = SafeJava(a,b)

  def GoodOldJava(impure: String => Int, str: String): Try[Int] = Try(impure(str)).toEither match {
    case Right(k)=> Success(k)
    case Left(l) => Success(0)
  }

  def testGoodOldJava(impure: String => Int, str: String): Try[Int] = GoodOldJava(impure,str)

  def main(args: Array[String]): Unit = {
    println(testSafeDivide(5,2))
  }
}



