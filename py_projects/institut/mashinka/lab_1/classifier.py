import numpy as np
from distance import EuclideanDistance
from kernel import EpanechnikovKernel


class ParzenWindowClassifier:
    
    def __init__(self,k):
        self._validate_k(k)
        self.k = k
        self.kernel = EpanechnikovKernel()
        self.distance = EuclideanDistance()
        
    def fit(self, X, Y):
        self._validate_X_Y(X, Y)
        self.X_train = X
        self.y_train = Y

    def predict_one(self, x):
        self._validate_predict_one_start(x)

        #считаем расстояния
        distances = []
        for i in range(len(self.X_train)):
            dist = self.distance.calculate(x, self.X_train[i])
            distances.append(dist)

        #аргсорт отсортирует массив и вернет нам индексы
        order = np.argsort(distances)
        h = distances[order[self.k]]

        #частный случай
        if h == 0:
            exact_labels = []
            for i in range(len(distances)):
                if distances[i] == 0:
                    exact_labels.append(self.y_train[i])

            counts = {}
            for i in range (len(exact_labels)):
                if exact_labels[i] not in counts:
                    counts[exact_labels[i]] = 0
                counts[exact_labels[i]] += 1
            return max(sorted(counts), key=counts.get)
        
        weights = []

        for i in range(len(distances)):
            weights.append(self.kernel.calculate(distances[i] / h))

        scores = {}

        for i in range(len(weights)):
            if self.y_train[i] not in scores:
                scores[self.y_train[i]] = 0.0
            scores[self.y_train[i]] += weights[i]
        return max(sorted(scores), key=scores.get)

    def predict(self, X):
        self._validate_predict_start(X)
        predictions = []
        for i in range(len(X)):
            predictions.append(self.predict_one(X[i]))
        return np.array(predictions)
    
    def _validate_k(self,k):
        if not isinstance(k,int):
            raise TypeError("TypeErrorK")
        if k <= 0:
            raise ValueError("ValueErrorK")

    def _validate_X_Y(self,X,Y):
        if not isinstance(X, np.ndarray) or not isinstance(Y, np.ndarray):
            raise TypeError("X и Y должны быть numpy массивами")
        
        if X.ndim != 2:
            raise ValueError("нарушена двумерность X")

        if Y.ndim != 1:
            raise ValueError("нарушена одномерность Y")

        if len(X) != len(Y):
            raise ValueError("DataLengthError")
        if len(X) < self.k + 1:
            raise ValueError("недостаточно данных для классификации")
        
        if not np.issubdtype(X.dtype, np.number):
            raise TypeError("DataNumError: X")

        if not np.issubdtype(Y.dtype, np.number):
            raise TypeError("DataNumError: Y")

    def _validate_predict_one_start(self,x):
        if not hasattr(self, "X_train"):
            raise ValueError("Отсутствует аттрибут X_train")
        if not isinstance(x, np.ndarray):
            raise TypeError("x должен быть numpy массивом")
        if x.ndim != 1:
            raise ValueError("нарушена одномерность x")
        if len(x) != self.X_train.shape[1]:
            raise ValueError("Неверное кол-во признаков у нового цветка")

    def _validate_predict_start(self,X):
       if not isinstance(X, np.ndarray):
            raise TypeError("X должен быть numpy массивом")
       if X.ndim != 2:
            raise ValueError("нарушена двумерность X")