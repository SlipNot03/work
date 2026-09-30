#include <iostream>
#include <vector>

using namespace std;

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n;
    cin >> n;

    vector<long long> prefix(n + 1, 0);

    for (int i = 1; i <= n; i++) {
        long long x;
        cin >> x;
        prefix[i] = prefix[i - 1] + x;
    }

    int m;
    cin >> m;

    for (int i = 0; i < m; i++) {
        int left, right;
        cin >> left >> right;

        if (left > right) {
            int temp = left;
            left = right;
            right = temp;
        }

        cout << prefix[right] - prefix[left - 1] << '\n';
    }

    return 0;
}

/*
Пример входных данных:
5
1 2 3 4 5
3
1 5
2 4
3 3

Пример выходных данных:
15
9
3

Сложность:
по времени: O(n + m);
по памяти: O(n), для массива префиксных сумм.
*/
