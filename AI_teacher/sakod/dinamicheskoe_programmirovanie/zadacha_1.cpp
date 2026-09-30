#include <iostream>
#include <vector>

using namespace std;

const long long MOD = 1000000007LL;

int main() {
    int n, k;
    cin >> n >> k;

    vector<long long> dp(n + 1, 0);
    dp[0] = 1;

    for (int step = 1; step <= n; step++) {
        for (int jump = 1; jump <= k; jump++) {
            if (step - jump >= 0) {
                dp[step] = (dp[step] + dp[step - jump]) % MOD;
            }
        }
    }

    cout << dp[n];

    return 0;
}

/*
Пример входных данных:
5 3

Пример выходных данных:
13

Сложность:
по времени: O(n * k);
по памяти: O(n), для массива dp.
*/
