# Cores-Inteligentes
Resumo: utilização de algoritmos de busca inteligentes com o intuito de formar uma paleta de cores.

Observação: a parte publicada nesse repositório inclui uma parcial do trabalho final para a disciplina Inteligência Artificial, programado por mim. Ainda há, fora desse repositório (a ser incluída posteriormente), a implmentação do A* e heurísticas infladas, além do uso de uma biblioteca para visualização do grafo de cores. 

Os participantes do grupo são: Danilo Barcellos Corrêa, Igor Akaida, Leonardo Basset Figueiredo Pereira, Sean Torres dos Santos e Sérgio Carlos de Sousa Gregório Júnior.

# 1 Do Problema e Uso de Inteligência Artificial
O problema escolhido e desenvolvido consiste em formar uma paleta de cores, considerando uma cor inicial e uma cor final escolhidas pelo usuário, como se observa na Figura 1. A transição deve ser realizada considerando a luminosidade das cores iniciais e finais. Ou seja, se a cor inicial é escura, e a final, clara, as intermediárias devem ir clareando até o destino final. Além disso, o usuário escolhe o número de cores dessa transição (recomendamos, porém, um número menor ou igual a 5).

![PlanejamentoGeral - Frame 9](https://github.com/user-attachments/assets/bee62a46-103c-411d-bad6-7703dcf33187)

Utilizamos um grafo para representar os possíveis caminhos da cor inicial até a final. Dessa forma, para a escolha do caminho mínimo, foi implementado o algoritmo Greedy Best First, algoritmo guloso para selecionar o menor caminho possível (apesar de não estar incluído na implementação, pode-se facilmente atualizá-la para incluir o caminho máximo). O menor caminho possível aponta para cores mais semelhantes entre si.

# 2 O Algoritmo para Gerar Cores
Nesse sentido, escolhemos pela geração de um grafo, a fim de listar caminhos possíveis para a formação da paleta de cores. Cada nó representa uma cor RGB (Red, Green e Blue), e o valor das arestas é a distância euclidiana entre uma cor e outra.

Cada nó possui 3 filhos, exceto os penúltimos nós, os quais possuem como único filho a cor destino, como se observa na Figura 2. Quanto à geração das cores intermediárias, considerando um nó pai, há a preservação de R no primeiro filho, G no segundo filho e B no terceiro. Faz-se a média de R, G e B do nó pai, e essa média é incluída nos nós filhos. Como cada cor possuem 3 atributos, RGB, o terceiro valor a ser incluído é calculado respeitando a transição da luminosidade.

# 3 
