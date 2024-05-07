# AI Agent Simulation

## Autores
#### Daniela Dantas, a22202104
- Criou o mapa da simulação;
- Implementou os Behaviors dos NPCs;
- [etc]

#### João Correia, a22202506
- Efetuou a pesquisa de artigos sobre a simulação de tráfego urbano;
- Escreveu o Relatório;
- [etc]

## Introdução
Este projeto visou criar e simular um modelo de trânsito urbano que retrata realisticamente o trânsito pedonal e de veículos em ambiente citadino, com o objetivo de observar os comportamentos e interações que os vários agentes da simulação têm entre si, enquanto mantendo um ambiente ordenado e regulamentado (seguindo e obedecendo a regras impostas, como sinais ou passadeiras). Prentendeu-se também observar estas interações num ambiente mais caótico, implementando um estado de descontrolo de agentes que pode ser manualmente ativado pelo utilizador da simulação, com a intenção de examinar o comportamento dos restantes agentes perante outros que estejam descontrolados, os quais quebram as regras impostas e causam transtorno aos objetivos como, por exemplo, causando acidentes e bloqueando o acesso a certos destinos no mapa. Por fim, pretendeu-se também conseguir que os agentes da simulação conseguissem re-establecer a normalidade da simulação após qualquer uma destas situações de caos.

Foram estudados vários artigos e documentos acerca do tema de simulação de tráfego em cidades, nomeadamente 4 artigos com implementações de modelos semelhantes ao deste projeto, que utilizam dados reais de veículos, condutores/passageiros e respetivos comportamentos e rotinas. Esta pesquisa permitiu ter uma referência sólida sobre as técnicas e boas-práticas usadas em projetos e modelos semelhantes, de modo a conseguir uma simulação mais fiel e bem estruturada para a implementação dos requisitos e objetivos impostos para este projeto.

Portanto, para alcançar os objetivos da implementação, programou-se o comportamento de agentes móveis (veículos e peões), os quais se movem entre diversos lugares da cidade, e agentes imóveis (semáforos, sinais de paragem, e passadeiras), que condicionam este movimento. Depois, colocaram-se estes agentes num mapa que simula uma cidade, com vários objetivos e destinos para os agentes móveis se deslocarem, de modo a observar os seus comportamentos e interações com os outros agentes. Por fim, implementou-se o estado de descontrolo, que proporciona os agentes descontrolados a quebrarem as regras dos seus comportamentos, causando caos na simulação. Utilizando vários parâmetros costumizáveis, observou-se o estado da simulação e o comportamento dos agentes, arranjando quaisqueres falhas nos seus raciocínios e garantindo que estes fossem capazes de voltar à normalidade após qualquer acidente ou estado de caos.

[Resultados Obtidos]

## Estado da Arte
Comparativamente ao objetivo deste projeto, existem vários outros artigos e projetos com temas semelhantes que visam também examinar os comportamentos do trânsito urbano, não só para obter estimativas e conclusões acerca do comportamento de peões e/ou veículos, como também para observar as falhas e as decisões da própria Inteligência Artificial usada nos modelos de simulação, especialmente nesta nova era do mundo em que as técnologias de IA estão a revolucionar-se e a evoluir rápidamente a cada dia que passa. Eis alguns artigos de pesquisa relevantes que foram utilizados como base de estudo e referência para efetuar este projeto:

#### ["An Overview of Agent-Based Models for Transport Simulation and Analysis"](https://doi.org/10.1155/2022/1252534)
###### por Jiangyan Huang, Youkai Cui, Lele Zhang, Weiping Tong, Yunyang Shi e Zhiyuan Liu
Este artigo expõe uma visão geral da maioria dos modelos de simulação de tráfico urbano, focando-se nas técnicas e processos normalmente usados para a Inteligência Artificial dos agentes. Os autores apresentam um resumo da estrutura básica dos agentes para a criação de uma simulação de trânsito citadino, tendo como base várious outros modelos estudados, referindo as formas mais corretas e eficientes para criar o movimento e comportamentos dos agentes e dos transportes. Referem também alguns aspetos que podem ser melhorados de um modo geral, como a implementação de Machine Learning nos agentes para reforçar a sua autonomia, bem como a implementação de comportamentos de cooperação (ou conflitos em grupo) entre agentes, ou seja, ações que requerem mais do que um agente para serem realizadas, de modo a aumentar o realismo das simulações. A maior parte dos modelos estudados (bem como o modelo realizado neste projeto) retrata cada agente como uma entidade singular que só se conhece a sí e não considera os outros agentes (a menos que causem uma alteração no seu comportamento ou o impeçam de concluir uma ação), tornando assim quase impossível de simular comportamentos humanos que só são possíveis quando os agentes interagem entre sí (por exemplo, numa grande fila de trânsito, é mais provável que um condutor perca a paciência se vários outros condutores estiverem a buzinar, ou se vários peões estiverem a fugir de algo, um agente que veja a multidão mas não veja o perigo pode começar a fugir também, simulando assim o medo coletivo).

Comparado com este projeto, este artigo revela-se uma importante referência, pois apresenta um estudo de vários modelos semelhantes a este. Incluindo as boas práticas usadas em cada um resumidas numa visão geral, o artigo apresenta um modelo-base dos agentes e também dos transportes (veículos), sendo uma boa referência para a criação da Inteligência Artificial na realização deste projeto.

#### ["CityFlowER: An Efficient and Realistic Traffic Simulator with Embedded Machine Learning Models"](https://doi.org/10.48550/arXiv.2402.06127)
###### por Longchao Da, Chen Chu, Weinan Zhang e Hua Wei
Este artigo apresenta um modelo de simulação de tráfico urbano, "CityFlowER", que visa ser uma versão melhorada e mais realista do atual modelo "CityFlow". O artigo mostra como o novo CityFlowER utiliza modelos de Machine Learning para determinar as decisões de cada agente, promovendo a individualidade de cada um e permitindo que se implementem vários modelos de Machine Learning diferentes no agentes, ao contrário do CityFlow atual, o qual determina o comportamento de todos os agentes com base num único conjunto de regras.

Este projeto apresenta uma estrutura de regras de comportamentos de agentes semelhante à forma como está implementada no modelo CityFlow, portanto embora o artigo se foque no CityFlowER e nas suas vantagens, os autores referem também várias referências de como funcionam as várias componentes do CityFlow original, visto que precisavam de fazer comparações do seu novo modelo com o antigo. Isto revela-se importante para a pesquisa deste projeto porque, novamente, a implementação dos comportamentos dos agentes do CityFlow é semelhante a este projeto, e os dados e as comparações presentes neste artigo são uma boa referência das técnicas usadas, o que é útil para a implementação das componentes deste trabalho.

#### ["Agent-based modelling with geographically weighted calibration for intra-urban activities simulation using taxi GPS trajectories"](https://doi.org/10.1016/j.jag.2023.103368)
###### por Shuhui Gong, Xiangrui Dong, Kaiqi Wang, Bingli Lei, Zizhao Jia, Jiaxin Qin, Chris Roadknight, Yu Liu e Rui Cao
Este artigo retrata um modelo de simulação de tráfico citadino criado usando dados recolhidos por condutores de táxis, conseguindo assim recriar uma estrutura de regras e comportamentos realistas, com base nas rotinas dos condutores e dos respetivos passageiros. Ao identificar e estudar as rotinas dos cidadãos, foi possível criar um modelo de IA fiel à realidade, visto que os dados foram recolhidos em tempo real de peões/passageiros reais. Utilizando 1.5 milhões de dados de viagens recolhidos de táxis, bem como outros registos de viagens anteriores, foi possível determinar os pontos de partida e os destinos de cada viagem, bem como a hora exata da cada um, permitindo criar vários perfis de passageiros, as suas rotinas, e até as áreas de residência de cada um. Estes dados foram usados para criar um modelo de regras de comportamentos e destinos dos vários agentes, com vários parâmetros como, por exemplo, movimentos em horas de ponta, ou se é dia de semana ou fim de semana (ou até feriados).

Comparativamente a este projeto, o modelo criado pelos autores deste artigo apresenta uma estrutura bastante semelhante (os peões e veículos viajam de objetivo para objetivo), embora o artigo apresente muitos mais parâmetros do que os implementados neste projeto. Mesmo assim, é bastante interessante e uma boa referência para a implementação da IA dos agentes, tendo usado dados recolhidos pessoalmente dos passageiros para criar os seus perfis e rotinas, bem como usando os trajetos dos táxis para melhorar as trajetórias mais eficientes de cada agente.

#### ["HetroTraffSim: A Macroscopic Heterogeneous Traffic Flow Simulator for Road Bottlenecks"](https://doi.org/10.3390/futuretransp3010022)
###### por Ali Zeb, Khurram S. Khattak, Muhammad Rehmat Ullah, Zawar H. Khan e Thomas Aaron Gulliver
Tendo em conta as previsões de crescimento da população a nível mundial, este artigo apresenta um modelo de simulação de tráfico em congestionamentos da estrada em situações com elevados números de agentes, visando prever futuras soluções para o elevado trânsito em zonas e/ou situações de congestionamento de veículos e transportes na estrada. Utilizando vários parâmetros como as dimensões dos vários veículos, as suas velocidades mínimas e máximas, as dimensões da própria estrada, e a visão dos condutores (usando raycasts), os autores criaram uma implementação deste modelo em Unity3D, permitindo simular, determinar, prever e até reduzir o trânsito em vários modelos de estrada como rotundas, cruzamentos, entroncamentos, zonas de inversão de marcha, estradas retas, entre outros.

Como a implementação deste modelo foi feita em Unity3D, que é o mesmo engine usado para o desenvolvimento deste projeto, este artigo revela ser uma referência extremamente importante para a sua realização, pois os autores explicam com detalhe as técnicas que usaram para a implementação do seu modelo. Os dados dos comportamentos dos agentes em situações de tráfego elevado com bastantes agentes revela-se também importante para a implementação dos comportamentos dos veículos no modelo deste projeto, pois alguns dos parâmetros que se tencionam atingir é conseguir ter um elevado número de agentes na simulação e/ou tê-los em um estado de caos e descontrolo, portanto a implementação neste artigo é uma referência muito útil para este trabalho.

## Metodologia
Este projeto foi implementado em Unity em 3D, tendo-se usando um mapa simples que contém apenas passeios, estradas e localizações (edifícios, parques, garagens, etc...). Criaram-se várias classes para o efeito, sendo estas a classe "Agent", que determina os agentes da simulação, a classe "Destination", que determina os vários objetivos para os quais os agentes móveis se deslocam, e as classes "RoadWaypoint", "RoadConnector" e "Crosswalk", as quais são usadas para construir as estradas do mapa, ligando os pedaços de estrada e definindo passadeiras de peões.

A classe "Destination" contém uma lista para guardar todos os agentes presentes dentro do destino, e guarda também as posições dos pontos de saída dos mesmos. Esta classe garda referências a componentes de TextMeshPRO para se visualizar durante a simulação o número total de cada tipo de agentes acima de cada destino, e contém também métodos para adicionar e remover agentes do destino (para adicioná-los à lista de agentes lá dentro e desativar o respetivo agente na simulação, e para removê-los da lista e ativar o agente novamente, movendo-o para a sua posição de saída).

A classe "Agent" é uma classe-base vazia, determinando apenas o que é um agente na simulação. Para criar os diferentes tipos de agentes, aesta classe foi extendida por duas subclasses: "MobileAgent" e "StaticAgent". Estas separam e definem os agentes que se movem no mapa e os que permanecem no mesmo local, respetivamente, e também estas subclasses froam extendidas para criar os diferentes agentes de cada tipo, tendo as classes "Pedestrian" e "Veichle" extendido "MobileAgent" para determinar os peões e os veículos, e a classe "TrafficLight" extendido "StaticAgent" para determinar os semáforos. Por fim, criou-se a interface "ITrafficLightListener" para definir quais dos outros agentes verificam o estado dos semáforos na sua proximidade, nesta caso afetando apenas os peões e os veículos. O seguinte diagrama de classes ilustra este procedimento:

![Diagrama de Classes da classe Agent](https://i.postimg.cc/jj68PpKF/Agent-Class-Diagram-drawio.png)

De seguida, adicionaram-se as árvores de comportamento e decisão de cada agente, bem como um estado de "descontrolo" que aumenta a probabilidade de o agente afetado não seguir as regras e causar caos na simulação. O comportamento dos agentes móveis é simples: movem-se entre objetivos, escolhidos aleatóriamente e um de cada vez, e após chegar ao destino, entram nele e permanecem lá durante algum tempo aleatório, com um máximo de tempo definido por outra classe que contém os parâmetros da simulação. Quando esse tempo acaba, os agentes saem desse destino e escolhem um novo objetivo para se deslocarem, diferente do último (para impedir que o agente escolha deslocar-se para o local de onde acabou de sair). Pelo caminho, os agentes respeitam sinais de trânsito, semáforos e passadeiras, e evitam colidir entre sí (havendo maior tolerância em colisões entre peões), porém os agentes têm uma maior chance de ignorar estas regras caso estejam "descontrolados", o que pode causar acidentes e causar a imobilização dos agentes afetados durante um período de tempo aleatório, cujo máximo de tempo é também definido pela outra classe anteriormente referida. Já os agentes estáticos apenas alternam entre os seus diferentes estados possíveis, caso existam (por exemplo, os semáforos alternam entre as suas cores, mas os sinais são sempre iguais).

### [Inserir behavior trees e state machines de todos os agentes]
![](link.png das imagens)

Já as classes "RoadWaypoint" e "RoadConnector" são usadas para definir e ligar as estradas, para os veículos se movimentarem corretamente por elas e seguirem as regras de circulação. A classe "RoadWaypoint" define os pontos por onde os veículos se dirigem e movimentam, cada um guardando um limite de velocidade nessa estrada, uma lista dos diferentes pontos de ligação da estrada, e guardando uma lista para registar todos os veículos ativos. A classe "RoadConnector" define os pontos de cada pedaço de estrada para ligar a outro, de modo a construir uma rede de estradas interligada, guardando o RoadWaypoint a que estão associados e uma lista de outras ligações possíveis.

Por fim, para inicializar e gerir os pârametros principais da simulação, criou-se a classe "SimulationManager", que é um singleton e apresenta os parâmetros principais no editor do Unity para permitir a sua costumização antes de se iniciar a simulação. Os pârametros são:

| Parâmetro | Descrição |
| ------ | ------ |
| numberOfVehicles | Define o número total de veículos na simulação. |
| numberOfPedestrians | Define o número total de peões na simulação. |
| vehicleDestinationMaxTime | Define o tempo máximo que um veículo pode permanecer num destino. |
| pedestrianDestinationMaxTime | Define o tempo máximo que um peão pode permanecer num destino. |
| accidentMaxTime | Define o tempo máximo que um agente pode ficar parado após um acidente. |
| uncontrolledMaxTime | Define o tempo máximo que um agente pode ficar no estado de "descontrolo". |

Ao iniciar a simulação, esta classe inicializa o número definido de cada tipo de agente e coloca-os aleatóriamente dentro dos vários destinos, e depois encarrega-se de gerir quanto tempo os vários agentes demoram a mudar de estados. Os agentes verificam este tempo através da classe SimulationManager, portanto os parâmetros podem ser alterados durante a simulação para alterar em tempo real essas durações em todos os agentes.

## Resultados e discussão
- Apresentação dos resultados, salientando os aspetos mais interessantes que
observaram na simulação, em particular se observaram comportamento emergente, isto é, comportamento que não foi explicitamente programado nos agentes.
- Caso tenham experimentado diferentes parâmetros podem apresentar quadros, tabelas e/ou gráficos com informação que considerem importante e/ou
interessante.
- Na parte da discussão, devem fazer uma interpretação dos resultados que
observaram, realçando quaisquer correlações que tenham encontrado entre
estes e as parametrizações que definiram, bem como resultados inesperados,
propondo hipóteses explicativas.

[Resultados observados]

[Interpretação desses resultados]

## Conclusões
- Nesta secção devem relacionar o que foi apresentado na introdução, nomeadamente o problema que se propuseram a resolver, com os resultados que
obtiveram, e como o vosso projeto e a vossa abordagem se relaciona no panorama geral da pesquisa que efetuaram sobre simulação por agentes de tráfego
em cidades.
- Uma pessoa que leia a introdução e conclusão do vosso relatório deve ficar
com uma boa ideia daquilo que fizeram e descobriram, embora sem saber os
detalhes.

[Relação dos resultados obtidos com a Introdução]

[Relação com a pesquisa efetuada]

## Referências

- Da, L., Chu, C., Zhang, W., & Wei, H. (2024, February 9). CityFlowER: An Efficient and Realistic Traffic Simulator with Embedded Machine Learning Models. *Cornell University*. https://doi.org/10.48550/arXiv.2402.06127
- Gong, S., Dong, X., Wang, K., Lei, B., Jia, Z., Qin, J., Roadknight, C., Liu, Y., & Cao, R. (2023, May 23). Agent-based modelling with geographically weighted calibration for intra-urban activities simulation using taxi GPS trajectories. *International Journal of Applied Earth Observation and Geoinformation*. https://doi.org/10.1016/j.jag.2023.103368
- Huang, J., Cui, Y., Zhang, L., Tong, W., Shi, Y., & Liu, Z. (2022, February 27). An Overview of Agent-Based Models for Transport Simulation and Analysis. *Journal of Advanced Transportation*. https://doi.org/10.1155/2022/1252534
- Zeb, A., Khattak, K. S., Ullah, M. R., Khan Z. H., & Gulliver T.A. (2023, March 10). HetroTraffSim: A Macroscopic Heterogeneous Traffic Flow Simulator for Road Bottlenecks. *Future Transportation*. https://doi.org/10.3390/futuretransp3010022