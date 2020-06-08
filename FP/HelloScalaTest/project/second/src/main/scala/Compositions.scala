package exercise2
sealed trait Option[A]
{
  def map[B](f: A => B): Option[B]
  def flatmap[B](f: A => Option[B]):Option[B]
}
case class Some[A](a: A) extends Option[A]
{
  def map[B](f: A => B): Option[B] = Some(f(a))
  def flatmap[B](f: A => Option[B]):Option[B] = f(a)
}
case class None[A]() extends Option[A]
{
  def map[B](f: A => B): Option[B] = None()
  def flatmap[B](f: A => Option[B]):Option[B] = None()
}
object Compositions {
  def testCompose[A,B,C,D](f: A=>B)(g: B=>C)(h: C=>D): A => D = h compose g compose f
  def testMapFlatMap[A,B,C,D](f: A=>Option[B])(g: B=>Option[C])(h: C=>D): Option[A] => Option[D] = _ flatmap f flatmap g map h
  /*def testForComprehension[A,B,C,D](f: A=>Option[B])(g: B=>Option[C])(h: C=>D): Option[A] => Option[D] = {
    v =>
      for {
        first <- v
        second <- f(first)
        third <- g(second)
    } yield h(third)
  }*/
  }
