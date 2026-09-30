require_relative "Function/distribution"
require_relative "Function/interval"
require_relative "Function/gistogramm"
require_relative "Function/func_distribution"
require_relative "Function/graf_func_distr"
require_relative "Function/main_character"



# Задание 2. По данным дискретной выборки требуется:
# - составить статистическое распределение и ряд относительных частот;
# - построить полигон частот и эмпирическую функцию распределения;
# - найти выборочное среднее, дисперсию, среднее квадратическое отклонение и коэффициент вариации.
# Вариант 1: берутся элементы выборки из 10 следующих строк таблицы, начиная с 1-й; объем выборки n = 150.


#Я составляю частоты, относительные частоты, эмпирическую функцию и графики

def main
  # data - выборка с простыми числами
  data = [
    2, 4, 2, 4, 3, 3, 3, 2, 0, 6, 1, 2, 3, 2, 2,
    1, 3, 3, 3, 2, 0, 6, 0, 2, 4, 3, 2, 1, 6, 2,
    2, 1, 2, 3, 2, 2, 4, 3, 1, 4, 5, 3, 4, 3, 1,
    0, 2, 5, 3, 3, 1, 6, 2, 4, 5, 2, 4, 2, 4, 3,
    4, 3, 1, 4, 5, 3, 0, 2, 4, 3, 2, 3, 4, 3, 1,
    2, 3, 4, 0, 2, 5, 3, 3, 3, 3, 2, 0, 6, 2, 3,
    1, 5, 2, 4, 2, 4, 3, 1, 2, 3, 2, 2, 2, 3, 4,
    1, 6, 2, 3, 3, 2, 0, 6, 2, 5, 0, 2, 4, 3, 2,
    6, 0, 2, 5, 3, 3, 3, 5, 4, 3, 1, 4, 5, 4, 3,
    2, 1, 2, 3, 2, 2, 0, 2, 5, 3, 3, 1, 6, 2, 4
  ]
  # max(data)-min(data) - размах, то есть максимум минус минимум.
  puts(data.max - data.min)
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
