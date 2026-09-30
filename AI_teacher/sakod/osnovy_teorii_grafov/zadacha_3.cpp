#include <iostream>
#include <vector>
#include <string>
#include <sstream>

using namespace std;

void dfs(int vertex, const vector<vector<int> >& graph, vector<int>& used,
         vector<int>& enterTime, vector<int>& exitTime, int& timer) {
    used[vertex] = 1;
    timer++;
    enterTime[vertex] = timer;

    for (int i = 0; i < (int)graph[vertex].size(); i++) {
        int to = graph[vertex][i];
        if (used[to] == 0) {
            dfs(to, graph, used, enterTime, exitTime, timer);
        }
    }

    timer++;
    exitTime[vertex] = timer;
}

int main() {
    int n, m;
    cin >> n >> m;
    cin.ignore();

    vector<vector<int> > graph(n);

    for (int i = 0; i < n; i++) {
        string line;
        getline(cin, line);

        stringstream input(line);
        int to;

        while (input >> to) {
            graph[i].push_back(to - 1);
        }
    }

    vector<int> used(n, 0);
    vector<int> enterTime(n, 0);
    vector<int> exitTime(n, 0);
    int timer = 0;

    for (int i = 0; i < n; i++) {
        if (used[i] == 0) {
            dfs(i, graph, used, enterTime, exitTime, timer);
        }
    }

    for (int i = 0; i < n; i++) {
        cout << enterTime[i] << ' ';
    }
    cout << '\n';

    for (int i = 0; i < n; i++) {
        cout << exitTime[i] << ' ';
    }

    return 0;
}

/*
Пример входных данных:
4 4
2 3
1 3
1 2 4
3

Пример выходных данных:
1 2 3 4
8 7 6 5

Сложность:
по времени: O(n + m), обход просматривает вершины и списки смежности;
по памяти: O(n + m), для списка смежности и массивов обхода.
*/
