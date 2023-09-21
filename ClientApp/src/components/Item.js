import { useEffect } from "react";

const Item = ({ item, drag, itemImgObj }) => {
  useEffect(() => {
    console.log("ITEM!!", item);
    if (!item) window.location.reload();
  }, [item, itemImgObj]);
  return (
    // <div>
    //     {
    //         item && item.image  ?
    //         (<div className="unranked-cell">
    //             <img id={`item-${item?.id}`} src={itemImgObj?.image}
    //                 style={{ cursor: "pointer" }} draggable="true" onDragStart={drag}
    //             />
    //         </div>)
    //         :
    //         (<div>loading..</div>)
    //     }
    // </div>
    <div className="unranked-cell">
      <img
        id={`item-${item?.id}`}
        src={itemImgObj?.image}
        style={{ cursor: "pointer" }}
        draggable="true"
        onDragStart={drag}
      />
    </div>
  );
};
export default Item;
