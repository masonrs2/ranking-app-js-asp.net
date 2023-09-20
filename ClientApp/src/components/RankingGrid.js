const RankingGrid = ({ items, imgArr, drag, allowDrop, drop }) => {
  const rankingGrid = [];
  const cellCollectionTop = [];
  const cellCollectionMiddle = [];
  const cellCollectionBottom = [];
  const cellCollectionWorst = [];

  function pushCellMarkupToArr(cellCollection, rankNum, rowLabel) {
    if (rankNum > 0) {
      var item = items.find((o) => o.ranking === rankNum);
      cellCollection.push(
        <div
          id={`rank-${rankNum}`}
          onDrop={drop}
          onDragOver={allowDrop}
          className="rank-cell"
        >
          {item != null ? (
            <img
              id={`item-${item.id}`}
              src={imgArr.find((o) => o.id === item.imageId)?.image}
              draggable="true"
              onDragStart={drag}
            />
          ) : null}
        </div>
      );
    } else { 
      cellCollection.push(
        <div className="row-label">
          <h4>{rowLabel}</h4>
        </div>
      );
    }
  }

  function createCellsForRow(rowNum) {
    const numCells = 5;
    const rowConfig = [
      { collection: cellCollectionTop, label: "Top Tier" },
      { collection: cellCollectionMiddle, label: "Middle Tier" },
      { collection: cellCollectionBottom, label: "Bottom Tier" },
      { collection: cellCollectionWorst, label: "Worst Tier" },
    ];
    const { collection: currCollection, label } = rowConfig[rowNum - 1];

    for (let a = 1; a <= numCells; a++) {
      const rankNum = a === 1 ? 0 : numCells * (rowNum - 1) + a - rowNum;
      pushCellMarkupToArr(currCollection, rankNum, label);
    }
  }

  function createCellsForRows() {
    [...Array(4)].forEach((_, index) => createCellsForRow(index + 1));
  }

  function createRowsForGrid() {
    const tiers = ['top', 'middle', 'bottom', 'worst'];
    const collections = [cellCollectionTop, cellCollectionMiddle, cellCollectionBottom, cellCollectionWorst];

    tiers.forEach((tier, index) => {
      rankingGrid.push(
        <div className={`rank-row ${tier}-tier`}>{collections[index]}</div>
      );
    });

    return rankingGrid;
  }

  function createRankingGrid() {
    createCellsForRows();
    return createRowsForGrid();
  }

  return <div className="rankings">{createRankingGrid()}</div>;
};
export default RankingGrid;
