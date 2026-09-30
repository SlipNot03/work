#include <iostream>
#include <vector>

using namespace std;

void siftDown(vector<int>& numbers, int size, int index) {
    while (true) {
        int left = index * 2 + 1;
        int right = index * 2 + 2;
        int smallest = index;

        if (left < size && numbers[left] < numbers[smallest]) {
            smallest = left;
        }

        if (right < size && numbers[right] < numbers[smallest]) {
            smallest = right;
        }

        if (smallest == index) {
            break;
        }

        int temp = numbers[index];
        numbers[index] = numbers[smallest];
        numbers[smallest] = temp;

        index = smallest;
    }
}

void heapSort(vector<int>& numbers) {
    int n = (int)numbers.size();

    for (int i = n / 2 - 1; i >= 0; i--) {
        siftDown(numbers, n, i);
    }

    for (int i = n - 1; i > 0; i--) {
        int temp = numbers[0];
        numbers[0] = numbers[i];
        numbers[i] = temp;

        siftDown(numbers, i, 0);
    }
}

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n;
    cin >> n;

    vector<int> numbers(n);
    for (int i = 0; i < n; i++) {
        cin >> numbers[i];
    }

    heapSort(numbers);

    for (int i = 0; i < n; i++) {
        cout << numbers[i] << ' ';
    }

    return 0;
}

/*
Пример входных данных:
5
5 4 8 4 3

Пример выходных данных:
8 5 4 4 3

Сложность:
по времени: O(n log n), потому что каждый элемент извлекается из пирамиды;
по памяти: O(1), сортировка выполняется внутри исходного массива.
*/
