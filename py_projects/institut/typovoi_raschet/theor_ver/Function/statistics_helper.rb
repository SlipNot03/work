
# Это общий файл с математическими помощниками: средние, дисперсии, функции распределений, критерии, корреляция и регрессия.

module StatisticsHelper
  module_function

  def fmt_number(value)
    number = value.to_f
    number == number.to_i ? number.to_i.to_s : number.to_s
  end

  def mean(data)
    data.sum.to_f / data.length
  end

  def variance(data, sample: true, center: nil)
    x = center || mean(data)
    denominator = sample ? data.length - 1 : data.length
    data.sum { |v| (v - x) ** 2 }.to_f / denominator
  end

  def std(data, sample: true)
    Math.sqrt(variance(data, sample: sample))
  end

  def frequencies(data)
    result = Hash.new(0)
    data.each { |x| result[x] += 1 }
    result.sort_by { |key, _| key }.to_h
  end

  def normal_cdf(x, mu = 0.0, sigma = 1.0)
    z = (x - mu).to_f / (sigma * Math.sqrt(2.0))
    0.5 * (1.0 + Math.erf(z))
  end

  # Обратная функция нормального распределения. Формула нужна вместо scipy.
  def normal_ppf(p)
    raise ArgumentError, "p must be between 0 and 1" unless p.positive? && p < 1.0

    a = [-39.69683028665376, 220.9460984245205, -275.9285104469687,
         138.3577518672690, -30.66479806614716, 2.506628277459239]
    b = [-54.47609879822406, 161.5858368580409, -155.6989798598866,
         66.80131188771972, -13.28068155288572]
    c = [-0.007784894002430293, -0.3223964580411365, -2.400758277161838,
         -2.549732539343734, 4.374664141464968, 2.938163982698783]
    d = [0.007784695709041462, 0.3224671290700398, 2.445134137142996,
         3.754408661907416]

    plow = 0.02425
    phigh = 1.0 - plow

    if p < plow
      q = Math.sqrt(-2.0 * Math.log(p))
      (((((c[0] * q + c[1]) * q + c[2]) * q + c[3]) * q + c[4]) * q + c[5]) /
        ((((d[0] * q + d[1]) * q + d[2]) * q + d[3]) * q + 1.0)
    elsif p <= phigh
      q = p - 0.5
      r = q * q
      (((((a[0] * r + a[1]) * r + a[2]) * r + a[3]) * r + a[4]) * r + a[5]) * q /
        (((((b[0] * r + b[1]) * r + b[2]) * r + b[3]) * r + b[4]) * r + 1.0)
    else
      q = Math.sqrt(-2.0 * Math.log(1.0 - p))
      -(((((c[0] * q + c[1]) * q + c[2]) * q + c[3]) * q + c[4]) * q + c[5]) /
        ((((d[0] * q + d[1]) * q + d[2]) * q + d[3]) * q + 1.0)
    end
  end

  def gammaln(xx)
    cof = [76.18009172947146, -86.50532032941677, 24.01409824083091,
           -1.231739572450155, 0.001208650973866179, -0.000005395239384953]
    x = xx
    y = xx
    tmp = x + 5.5
    tmp -= (x + 0.5) * Math.log(tmp)
    ser = 1.000000000190015
    cof.each do |c|
      y += 1.0
      ser += c / y
    end
    -tmp + Math.log(2.5066282746310005 * ser / x)
  end

  def gamma_p(a, x)
    return 0.0 if x <= 0.0

    if x < a + 1.0
      ap = a
      sum = 1.0 / a
      del = sum
      1000.times do
        ap += 1.0
        del *= x / ap
        sum += del
        break if del.abs < sum.abs * 1e-12
      end
      sum * Math.exp(-x + a * Math.log(x) - gammaln(a))
    else
      b = x + 1.0 - a
      c = 1.0 / 1e-30
      d = 1.0 / b
      h = d
      1.upto(1000) do |i|
        an = -i * (i - a)
        b += 2.0
        d = an * d + b
        d = 1e-30 if d.abs < 1e-30
        c = b + an / c
        c = 1e-30 if c.abs < 1e-30
        d = 1.0 / d
        del = d * c
        h *= del
        break if (del - 1.0).abs < 1e-12
      end
      1.0 - Math.exp(-x + a * Math.log(x) - gammaln(a)) * h
    end
  end

  def chi_square_cdf(x, df)
    gamma_p(df / 2.0, x / 2.0)
  end

  def chi_square_ppf(p, df)
    high = [df.to_f, 1.0].max
    high *= 2.0 while chi_square_cdf(high, df) < p
    low = 0.0
    100.times do
      mid = (low + high) / 2.0
      if chi_square_cdf(mid, df) < p
        low = mid
      else
        high = mid
      end
    end
    (low + high) / 2.0
  end

  def chi_critical(k, alpha = 0.01)
    chi_square_ppf(1.0 - alpha, k)
  end

  def poisson_pmf(k, lambda)
    Math.exp(-lambda) * (lambda ** k) / factorial(k)
  end

  def factorial(n)
    (1..n).reduce(1, :*)
  end

  def histogram(data, bins)
    counts = Array.new(bins.length - 1, 0)
    data.each do |value|
      (0...(bins.length - 1)).each do |i|
        last = i == bins.length - 2
        if value >= bins[i] && (value < bins[i + 1] || (last && value <= bins[i + 1]))
          counts[i] += 1
          break
        end
      end
    end
    counts
  end

  def linspace(start_value, end_value, count)
    return [start_value.to_f] if count <= 1

    step = (end_value - start_value).to_f / (count - 1)
    Array.new(count) { |i| start_value + step * i }
  end

  def regression_equation(x_values, y_values, print_roots: true)
    n = x_values.length
    sx = x_values.sum.to_f
    sy = y_values.sum.to_f
    sxy = x_values.zip(y_values).sum { |x, y| x * y }.to_f
    sx2 = x_values.sum { |x| x * x }.to_f
    a = (n * sxy - sx * sy) / (n * sx2 - sx * sx)
    b = (sy - a * sx) / n
    puts format("Корни системы: a = %.6f, b = %.6f", a, b) if print_roots
    [a, b]
  end

  def pearson_correlation(x_values, y_values)
    n = x_values.length
    mx = mean(x_values)
    my = mean(y_values)
    sx = Math.sqrt(x_values.sum { |x| (x - mx) ** 2 } / n.to_f)
    sy = Math.sqrt(y_values.sum { |y| (y - my) ** 2 } / n.to_f)
    cov = x_values.zip(y_values).sum { |x, y| (x - mx) * (y - my) } / n.to_f
    cov / (sx * sy)
  end

  def covariance(x_values, y_values)
    mx = mean(x_values)
    my = mean(y_values)
    x_values.zip(y_values).sum { |x, y| (x - mx) * (y - my) } / (x_values.length - 1).to_f
  end

  def r_squared(y_true, y_pred)
    mean_y = mean(y_true)
    ss_res = y_true.zip(y_pred).sum { |y, pred| (y - pred) ** 2 }
    ss_tot = y_true.sum { |y| (y - mean_y) ** 2 }
    1.0 - ss_res / ss_tot
  end
end
