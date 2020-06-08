package exercise1



class Animal(names: String, foods: String)
{
  var name: String = null
  var food: String = null

  def eats(food: String): Boolean={
    if (food.equals(null))false
    else true
  }
}

object Animal {
  var Mammals = ("cat","meat")
  var Birds = ("parrot","vegetable")
  var Fishs = ("goldfish","plants")

  trait Food {
    case object Meat extends Food
    case object Vegetables extends Food
    case object Plants extends Food

    def apply(food: String): Option[Food] =
      return Some.apply(Food.this)

  }

  trait Animal {
    case object Mammal extends Animal
    case object Fish extends Animal
    case object Bird extends Animal

    def knownAnimal(name: String): Boolean = {
      if ((Mammal == "cat") || (Bird == "parrot") || (Fish == "goldfish")) true
      else false
    }

    def apply(name: String): Option[Animal] =
      return Some.apply(Animal.this)

  }


  sealed trait Option[A]
  {
    def isEmpty: Boolean
  }
  case class Some[A](a: A) extends Option[A]{
    val isEmpty = false
  }
  case class None[A]() extends Option[A]{
    val isEmpty = true
  }
}





