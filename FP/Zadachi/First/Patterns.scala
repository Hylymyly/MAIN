/*package exercise1

object Patterns {

  sealed trait Hand
  case object  Paper extends Hand
  case object  Rock extends Hand
  case object  Scissor extends Hand

  sealed trait Result
  case object  Win extends Result
  case object  Lose extends Result
  case object  Draw extends Result

  sealed trait Food
  case object  Meat extends Food
  case object  Vegetables extends Food
  case object  Plants extends Food

  sealed trait Animal
  {
    val name: String
    val food: Food
  }
  case class Mammal(name: String, food: Food, weight: Int) extends Animal
  case class Fish(name: String, food: Food) extends Animal
  case class Bird(name: String, food: Food) extends Animal

  Mammal.apply("cat",Meat,11)
  Fish.apply("goldfish",Vegetables)
  Bird.apply("parrot",Vegetables)

  def numberOne(num: Int): String =
    {
      num match {
        case 1 => "it is one"
        case 2 => "it is two"
        case 3 => "it is three"
        case _ => "what's that"
      }
    }

  def testToString(value: Int): String = numberOne(value)

  def numberTwo(text: String): Boolean =
  {
    text match {
      case "max" => true
      case "Max" => true
      case "moritz" => true
      case "Moritz" => true
      case _ => false
    }
  }

  def testIsMaxAndMoritz(value: String): Boolean = numberTwo(value)

  def numberThree(value: Int): Boolean =
  {
    value%2 match{
      case 0 => true
      case 1 => false
    }
  }

  def testIsEven(value: Int): Boolean = numberThree(value)

  def numberFour(a: Hand, b: Hand): Result =
  {
    a match {
      case Rock => {
        b match{
          case Rock => Draw
          case Paper => Draw
          case Scissor => Draw
        }
      }
      case Paper => {
        b match{
          case Rock => Win
          case Paper => Draw
          case Scissor => Lose
        }
      }
      case Scissor => {
        b match{
          case Rock => Lose
          case Paper => Win
          case Scissor => Draw
        }
      }
    }
  }

  def testWinsA(a: Hand, b: Hand): Result = numberFour(a,b)

  def numberFive(animal: Animal): Int = {
   Animal match
     {
     case Mammal(name,food,weight) => weight
     case _ => -1
   }
  }

  def testExtractMammalWeight(animal: Animal): Int = numberFive(animal)

  def numberSix(animal: Animal): Animal = {
    Animal match
      {
      case Fish(name,food) => Fish(name,Plants)
      case Bird(name,food) => Bird(name,Plants)
      case Mammal(name,food,weight) => Mammal(name,food,weight)
    }
  }

  def testUpdateFood(animal: Animal): Animal = numberSix(animal)
}
*/