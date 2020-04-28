
/*
sealed trait List[A]
case class Cons[A](head: A, tail: List[A]) extends List[A]
case class Nil[A]() extends List[A]


sealed trait Tree[A]
case class Leaf[A](value: A) extends Tree[A]
case class Node[A](left: Tree[A],right: Tree[A]) extends Tree[A]

object RecursiveData {

  def ListIntEmpty(list: List[Int]): Boolean = list match{
    case Nil() => true
    case _ => false

  }

  def testListIntEmpty(list: List[Int]): Boolean = ListIntEmpty(list)

  def ListIntHead(list: List[Int]): Int = list match{
    case Cons(head,tail) => head
    case Nil() => -1
  }

  def testListIntHead(list: List[Int]): Int = ListIntHead(list)

  //c) Можно ли изменить List[A] так чтобы гарантировать что он не является пустым?
  // Можно, если воспользоваться типом ListBuffer, либо добавить значение "head" в класс Nil



 }*/
