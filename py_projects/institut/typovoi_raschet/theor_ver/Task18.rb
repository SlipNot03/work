require_relative "Function/my_methods"
require_relative "Function/plotter"


# Задание 18. Используя метод наименьших квадратов, найти уравнение прямой
# по экспериментальным данным и построить эту прямую.
# Сравнить полученный результат с расчетом готовым методом.
# Вариант 1: x = -3, -2, -1, 0, 1, 2, 3, 4, 5, 6; y = -2, -2, 1, 4, 5, 8, 8, 10, 14, 16.

#Я анализирую связь: Пирсон, Спирмен, регрессия, МНК, R2( коэффициент детерминации), ковариация

# X - значения x из таблицы.
X_TASK18 = [-3, -2, -1, 0, 1, 2, 3, 4, 5, 6]
# Y - значения y из таблицы.
Y_TASK18 = [-2, -2, 1, 4, 5, 8, 8, 10, 14, 16]

def main
  # a - наклон прямой, b - свободный член. Это наш метод.
  a, b = MyMethods.regression_equation_yx(X_TASK18, Y_TASK18)
  puts format("Уравнение прямой своим методом: y = %.4fx + %.4f", a, b)
  # slope_Y_on_X и intercept_Y_on_X - то же самое, но через готовую формулу.
  slope_y_on_x, intercept_y_on_x = StatisticsHelper.regression_equation(X_TASK18, Y_TASK18, print_roots: false)
  puts format("Уравнение c помощью готовых методов: Y = %.4f  X + %.4f", slope_y_on_x, intercept_y_on_x)

  Plotter.scatter_with_lines(
    X_TASK18,
    Y_TASK18,
    [
      {
        "x" => X_TASK18,
        "y" => X_TASK18.map { |x| a * x + b },
        "label" => "Уравнение прямой",
        "color" => "red"
      },
      {
        "x" => X_TASK18,
        "y" => X_TASK18.map { |x| slope_y_on_x * x + intercept_y_on_x },
        "label" => "Уравнение прямой готовым методом",
        "color" => "green"
      }
    ],
    "task18_regression",
    title: "Построение уравнения прямой методом наименьших квадратов"
  )
end

main if __FILE__ == $PROGRAM_NAME
