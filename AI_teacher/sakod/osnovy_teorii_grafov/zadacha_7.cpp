#include <iostream>
#include <vector>
#include <deque>
#include <queue>
#include <algorithm>

using namespace std;

struct Edge {
    int to;
    int weight;
};

bool compareEdges(const Edge& first, const Edge& second) {
    return first.to < second.to;
}

vector<int> zeroOneBfs(int start, const vector<vector<Edge> >& graph) {
    const int INF = 1000000000;
    int n = (int)graph.size();

    vector<int> distance(n, INF);
    deque<int> vertices;

    distance[start] = 0;
    vertices.push_front(start);

    while (!vertices.empty()) {
        int current = vertices.front();
        vertices.pop_front();

        for (int i = 0; i < (int)graph[current].size(); i++) {
            Edge edge = graph[current][i];
            int newDistance = distance[current] + edge.weight;

            if (newDistance < distance[edge.to]) {
                distance[edge.to] = newDistance;

                if (edge.weight == 0) {
                    vertices.push_front(edge.to);
                } else {
                    vertices.push_back(edge.to);
                }
            }
        }
    }

    return distance;
}

bool canReachFinish(int start, int finish, const vector<vector<Edge> >& graph,
                    const vector<int>& distanceToFinish, const vector<int>& used) {
    int n = (int)graph.size();
    vector<int> seen(n, 0);
    queue<int> vertices;

    seen[start] = 1;
    vertices.push(start);

    while (!vertices.empty()) {
        int current = vertices.front();
        vertices.pop();

        if (current == finish) {
            return true;
        }

        for (int i = 0; i < (int)graph[current].size(); i++) {
            Edge edge = graph[current][i];

            if (seen[edge.to] == 1 || used[edge.to] == 1) {
                continue;
            }

            if (edge.weight + distanceToFinish[edge.to] == distanceToFinish[current]) {
                seen[edge.to] = 1;
                vertices.push(edge.to);
            }
        }
    }

    return false;
}

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n, m;
    cin >> n >> m;

    vector<vector<Edge> > graph(n);

    for (int i = 0; i < m; i++) {
        int from, to, weight;
        cin >> from >> to >> weight;

        from--;
        to--;

        graph[from].push_back({to, weight});
        graph[to].push_back({from, weight});
    }

    int start, finish;
    cin >> start >> finish;
    start--;
    finish--;

    for (int i = 0; i < n; i++) {
        sort(graph[i].begin(), graph[i].end(), compareEdges);
    }

    vector<int> distanceToFinish = zeroOneBfs(finish, graph);
    vector<int> used(n, 0);
    vector<int> path;

    int current = start;
    path.push_back(current);

    while (current != finish) {
        used[current] = 1;

        for (int i = 0; i < (int)graph[current].size(); i++) {
            Edge edge = graph[current][i];

            if (used[edge.to] == 1) {
                continue;
            }

            if (edge.weight + distanceToFinish[edge.to] == distanceToFinish[current] &&
                canReachFinish(edge.to, finish, graph, distanceToFinish, used)) {
                current = edge.to;
                path.push_back(current);
                break;
            }
        }
    }

    cout << path.size() << ' ' << distanceToFinish[start] << '\n';

    for (int i = 0; i < (int)path.size(); i++) {
        cout << path[i] + 1 << ' ';
    }

    return 0;
}

/*
Пример входных данных:
4 5
3 4 0
1 3 1
2 4 1
1 2 0
2 3 0
1 4

Пример выходных данных:
4 0
1 2 3 4

Сложность:
по времени: O(n * (n + m)) в худшем случае из-за проверки достижимости при восстановлении пути;
по памяти: O(n + m), для списка смежности и служебных массивов.
*/
