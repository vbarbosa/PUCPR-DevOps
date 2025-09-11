# PUCPR-DevOps 🎮🌱
Disciplina: **DevOps**  
Repositório para o projeto da disciplina (PUCPR).

---

## 📖 Concepção do Projeto
Este projeto é um **jogo 2D estilo Farming/Survival**, desenvolvido em **Unity**, com foco em aprendizado prático de:
- Integração contínua e versionamento com Git/GitHub.
- Aplicação do fluxo GitFlow (branches `main`, `develop`, `feature/*`, `hotfix/*`).
- Organização de projeto colaborativo.
- Boas práticas em DevOps para projetos de software.

O jogo simula mecânicas básicas de um **farm game**, incluindo:
- Movimento e animação do Player.
- Sistema de câmera 2D com limites do mapa.
- Árvores interativas que podem ser cortadas com machado (Axe).
- Sistema de drops (madeira = `Log`).
- Inventário e Toolbar para gerenciamento de itens.
- Base para futuras expansões (plantio, crafting, economia).

---

## 🛠️ Stack Tecnológica
- **Engine:** Unity 2019.4.x LTS  
- **Linguagem:** C#  
- **Versionamento:** Git + GitHub  
- **Metodologia:** GitFlow (branches dedicadas para features e releases)  
- **Plataforma alvo:** PC (Windows)  

---

## 📂 Estrutura do Repositório
```
Assets/
 ├── Inventory/          # ScriptableObjects de itens (Axe, Log, Seeds, etc.)
 ├── Prefabs/            # Prefabs de Player, Tree, Log, Grid
 ├── Scenes/             # MainScene, TestScene
 ├── Scripts/            # Código-fonte C#
 │    ├── Character/     # PlayerControl, CameraMovement
 │    ├── Tree/          # TreeCuttable, ToolHit
 │    ├── Inventory/     # InventoryController, ItemContainer
 │    ├── Toolbar/       # ToolbarController
 │    └── Managers/      # GameManager, MoneyController
 └── Sounds/             # Áudios do jogo
```

---

## 🌳 Funcionalidades atuais
- ✅ Player com movimento e animação.  
- ✅ Câmera seguindo o Player com limites do mapa.  
- ✅ Árvores cortáveis com Axe (`TreeCuttable`).  
- ✅ Sistema de drop: `Log.prefab` instanciado ao destruir árvore.  
- ✅ Inventário funcional com Toolbar.  
- ✅ Sistema de coleta (`PickUpItem`).  
- ⚠️ Arquivos de áudio grandes precisam migrar para Git LFS.  
- 🚧 Próximas features: plantio, sementes, crafting.  

---

## 🚀 Como rodar o projeto
1. Clone o repositório:
   ```bash
   git clone https://github.com/vbarbosa/PUCPR-DevOps.git
   ```
2. Abra o projeto no **Unity Hub** (versão recomendada: `2019.4.12f1`).  
3. Abra a cena `Assets/Scenes/TestScene.unity`.  
4. Dê **Play** para testar.  

---

## 🔄 Fluxo de Desenvolvimento (GitFlow)
O projeto segue o fluxo **GitFlow**:

- **main** → versão estável (entregas finais).  
- **develop** → integração de features.  
- **feature/** → branches para novas funcionalidades.  
- **hotfix/** → correções rápidas em produção.  

Exemplo:
```bash
git checkout develop
git checkout -b feature/trees-system
# trabalha, commit, push...
git push origin feature/trees-system
# abre PR no GitHub para develop
```

---

## 📌 Contribuição
1. Crie uma branch a partir de `develop`:
   ```bash
   git checkout develop
   git checkout -b feature/nome-da-feature
   ```
2. Implemente sua feature.  
3. Faça commit e push.  
4. Abra um **Pull Request** para `develop`.  

---

## 📜 Licença
Este projeto foi desenvolvido para fins acadêmicos, como parte da disciplina de **DevOps (PUCPR)**.  
Uso livre para fins de estudo e aprendizado.
