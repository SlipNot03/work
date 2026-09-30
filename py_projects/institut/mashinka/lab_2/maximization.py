import numpy as np
from distance import EuclideanDistance


class Maximization:
    def __init__(self):
        distance = EuclideanDistance()
        self.distance = distance

    def calculate_one(self, X,expectations,cluster_number):
        cluster_points = X[expectations == cluster_number]
        if len(cluster_points) == 0:
            return None
        return np.mean(cluster_points, axis=0)

    def _find_farthest_point(self, X, expectations, old_centres,used_points):
        max_distance = -1
        max_distanc_object = None

        for i in range(len(old_centres)):
            cluster_points = X[expectations == i]

            for point in cluster_points:
                #на случай если пустых кластеров окажется больше одного
                point_already_used = any(np.array_equal(point, used_point) for used_point in used_points)

                if point_already_used:
                    continue

                distance = self.distance.calculate(
                    point,
                    old_centres[i],
                )

                if distance > max_distance:
                    max_distance = distance
                    max_distanc_object = point

        return max_distanc_object
    
    def calculate(self, X, expectations, old_centres):
        self._validate_calculate(X, expectations, old_centres)

        new_centres = []
        used_points = []

        for i in range(len(old_centres)):
            new_centre = self.calculate_one(
                X,
                expectations,
                i,
            )

            if new_centre is not None:
                new_centres.append(new_centre)
            else:
                #пустому кластеру назначаем самую далёкую точку
                farthest_point = self._find_farthest_point(X, expectations, old_centres, used_points)

                if farthest_point is None:
                    raise ValueError("Не удалось восстановить пустой кластер")

                new_centres.append(farthest_point)
                used_points.append(farthest_point)

        return np.array(new_centres)






    def _validate_calculate(self, X, expectations, old_centres):
        if not isinstance(X, np.ndarray) or not isinstance(expectations, np.ndarray) or not isinstance(old_centres, np.ndarray):
            raise TypeError("X, expectations и old_centres должны быть numpy массивами")
        
        if X.ndim != 2 or expectations.ndim != 1 or old_centres.ndim != 2:
            raise ValueError("нарушена размерность массивов")
        
        if not np.issubdtype(X.dtype, np.number) or not np.issubdtype(old_centres.dtype, np.number):
            raise TypeError("X и old_centres должны содержать числа")
        
        if len(old_centres) == 0:
            raise ValueError("old_centres должен содержать хотя бы один центр")
        
        if X.shape[1] != old_centres.shape[1]:
            raise ValueError("количество признаков в X и old_centres должно совпадать")
        
        if len(expectations) != len(X):
            raise ValueError("длина expectations должна совпадать с количеством объектов в X")
        
        if not np.issubdtype(expectations.dtype, np.integer):
            raise TypeError("expectations должен содержать целые числа")

        if np.any(expectations < 0) or np.any(expectations >= len(old_centres)):
            raise ValueError("неверные значения в expectations")

        if len(X) == 0:
            raise ValueError("X должен содержать хотя бы один объект")