package exercise3

object Sequence{

  def LastElement[A](seq: Seq[A]): Option[A] = {
    seq match {
      case last :: Nil => Option(last)
      case head :: tail => LastElement(tail)
    }
  }

  def testLastElement[A](seq: Seq[A]): Option[A] = LastElement(seq)

  def zipped[A](a: Seq[A],b: Seq[A]): Seq[(A,A)] = {
    def loop[A](a: Seq[A], b: Seq[A], c: Seq[(A,A)]): Seq[(A, A)] = a match {
      case ahead +: atail => b match {
        case blast +: Nil => c :+ (ahead,blast)
        case bhead +: btail =>loop(atail,btail,c:+(ahead,bhead))
      }
      case Nil => c
    }
    loop(a,b,Nil)
  }

  def testZip[A](a: Seq[A],b: Seq[A]): Seq[(A,A)] ={zipped(a,b)}

  def ForAll[A](seq: Seq[A])(cond: A=>Boolean): Boolean = {
      def loop[A](a: Seq[A])(cond: A=> Boolean): Boolean = a match{
      case head +: tail => if (cond(head)) loop(tail)(cond) else false
      case Nil => true
  }
    loop(seq)(cond)
  }

  def testForAll[A](seq: Seq[A])(cond: A=>Boolean): Boolean = ForAll(seq)(cond)

  def Palindrom[A](seq: Seq[A]): Boolean = {
    def loop[A](a:Seq[A],b:Seq[A]): Boolean = {
      a match {
        case head+:tail => loop(tail,b=head+:b)
        case Nil => seq.equals(b)
      }
    }
    loop(seq,Nil)
  }

  def testPalindrom[A](seq: Seq[A]): Boolean =Palindrom(seq)

  def FlatMap[A,B](seq: Seq[A])(f: A=>Seq[B]): Seq[B] = {
      seq.foldLeft[Seq[B]](Nil)((a,next)=>f(next).++(a))
  }

  def testFlatMap[A,B](seq: Seq[A])(f: A=>Seq[B]): Seq[B] = FlatMap(seq)(f)

  def main(args: Array[String]): Unit = {

  }
}