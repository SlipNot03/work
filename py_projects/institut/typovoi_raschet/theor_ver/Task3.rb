require_relative "Function/distribution"
require_relative "Function/interval"
require_relative "Function/gistogramm"
require_relative "Function/func_distribution"
require_relative "Function/graf_func_distr"
require_relative "Function/main_character"

# Задание 3. По данным выборки n = 100 требуется:
# - составить статистическое распределение выборки;
# - составить интервальный ряд относительных частот;
# - построить гистограмму относительных частот;
# - составить и построить эмпирическую функцию распределения;
# - найти основные числовые характеристики и пояснить их смысл.


#Я составляю частоты, относительные частоты, эмпирическую функцию и графики. В Task3 данные дробные, поэтому важна гистограмма.

def main
  # data - выборка из дробных чисел
  data = [
    2.0, 4.8, 5.2, 3.8, 3.5, 3.2, 3.2, 3.9, 4.9, 2.8,
    3.7, 1.8, 3.4, 2.3, 3.2, 4.5, 0.5, 3.3, 2.8, 2.5,
    1.4, 3.2, 3.5, 2.2, 2.3, 3.5, 3.5, 4.1, 4.4, 2.3,
    1.9, 2.2, 3.8, 3.4, 2.2, 3.1, 2.1, 2.1, 3.2, 2.5,
    2.1, 2.9, 2.8, 3.1, 4.3, 2.8, 4.0, 2.3, 2.7, 2.4,
    2.4, 2.3, 2.4, 2.9, 2.2, 3.6, 2.1, 3.2, 2.3, 2.9,
    2.0, 4.7, 3.5, 2.8, 3.0, -0.2, 3.6, 3.1, 3.3, 1.4,
    2.6, 2.6, 1.8, 4.3, 1.8, 0.7, 4.6, 3.0, 1.9, 3.7,
    3.2, 2.6, 2.6, 4.2, 2.9, 2.3, 5.4, 3.3, 3.1, 2.8,
    2.7, 2.7, 1.8, 2.8, 4.6, 2.7, 1.4, 3.9, 3.7, 2.5
  ]
  puts "p1"
  Distribution.main(data)
  puts "p2"
  Interval.main(data)
  puts "p3"
  Gistogramm.main(data)
  puts "p4"
  FuncDistribution.main(data)
  puts "p5"
  GrafFuncDistr.main(data)
  puts "p6"
  MainCharacter.main(data)
end

main if __FILE__ == $PROGRAM_NAME
