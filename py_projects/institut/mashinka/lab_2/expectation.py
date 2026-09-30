from distance import EuclideanDistance
import numpy as np


class Expectation:
    def __init__(self):
        self.distance = EuclideanDistance()

    def calculate_one(self, x, centres):
        distances = []
        for i in range(len(centres)):
            dist = self.distance.calculate(x, centres[i])
            distances.append(dist)
        #На случай если будут равные расстояния выбираем центр с меньшим индексом
        order = np.argsort(distances)
        return order[0]

    def calculate(self, X, centres):
        self._validate_calculate(X, centres)
        expectations = []
        for i in range(len(X)):
            expectations.append(self.calculate_one(X[i], centres))
        return np.array(expectations)




    def _validate_calculate(self,X,centres):
        if not isinstance(X, np.ndarray) or not isinstance(centres, np.ndarray):
            raise TypeError("X и centres должны быть numpy массивами")
        
        if X.ndim != 2 or centres.ndim != 2:
            raise ValueError("нарушена двумерность векторов")
        
        if not np.issubdtype(centres.dtype, np.number):
            raise TypeError("centres должен содержать числа")
        
        if len(centres) == 0:
            raise ValueError("centres должен содержать хотя бы один центр")
        
        if X.shape[1] != centres.shape[1]:
            raise ValueError("количество признаков в X и centres должно совпадать")