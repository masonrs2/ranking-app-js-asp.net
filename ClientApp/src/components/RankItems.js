import { useEffect, useState } from "react";
import RankingGrid from "./RankingGrid";
import ItemCollection from "./ItemCollection";

const RankItems = ({ items, setItems, dataType, imgArr, localStorageKey, database }) => {
  const [reload, setReload] = useState(false);
  const [update, setUpdate] = useState(false);

  function Reload() {
    setReload(true);
  }

  function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
  }

  function allowDrop(ev) {
    ev.preventDefault();
  }

  // function drop(ev) {
  //   ev.preventDefault();
  //   const targetElm = ev.target;
  //   if (targetElm.nodeName === "IMG") {
  //     return false;
  //   }
  //   if (targetElm.childNodes.length === 0) {
  //     var data = parseInt(ev.dataTransfer.getData("text").substring(5));
  //     const transformedCollection = items.map((item) =>
  //       item.id === parseInt(data)
  //         ? { ...item, ranking: parseInt(targetElm.id.substring(5)) }
  //         : { ...item, ranking: item.ranking }
  //     );
  //     // setItems(transformedCollection);

  //     // Update the item in the database
  //     const updatedItem = transformedCollection.find(item => item.id === parseInt(data));
  //     fetch(`/api/Item/${updatedItem.id}`, {
  //       method: 'PUT',
  //       headers: {
  //         'Content-Type': 'application/json',
  //       },
  //       body: JSON.stringify(updatedItem),
  //     })
  //     .then(response => response.json())
  //     .then(data => console.log(data))
  //     .catch((error) => {
  //       console.error('Error:', error);
  //     });

  //     fetch("/api/Item")
  //       .then((response) => response.json())
  //       .then((data) => {
  //         // Update the state with the fetched items
  //         setItems(data);
  //       })
  //       .catch((error) => console.error("Error:", error));
  //   }
  // }

  async function drop(ev) {
    ev.preventDefault();
    const targetElm = ev.target;
    if (targetElm.nodeName === "IMG") {
      return false;
    }
    if (targetElm.childNodes.length === 0) {
      var data = parseInt(ev.dataTransfer.getData("text").substring(5));
  
      // Update the item in the database
      const updatedItem = items.find(item => item.id === parseInt(data));
      updatedItem.ranking = parseInt(targetElm.id.substring(5));
      
      let response;
      try {
        if(dataType == 1){
          response = await fetch(`/api/${database}Item/${updatedItem.id}`, {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedItem),
          });
        }
        else if (dataType == 2){
           response = await fetch(`/api/${database}Item/${updatedItem.id}`, {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedItem),
          });
        }
  
        if (response.ok) { // Check if the response is ok
          const data = await response.text(); // Get the response as text
  
          if (data) { // If the response is not empty
            const jsonData = JSON.parse(data); // Parse the response as JSON
            console.log(jsonData);
          }
        } else {
          console.error('Error:', response.status);
        }
  
        // Fetch all items from the database
        const responseAll = await fetch(`/api/${database}Item`);
        const dataAll = await responseAll.json();
        console.log("NEW FETCHED DATA",dataAll)
        // Update the state with the fetched items
        setItems(dataAll);
        setUpdate(prevState => !prevState);
      } catch (error) {
        console.error('Error:', error);
      }
    }
  }

  async function fetchItems() {
    fetch(`/api/${database}Item`)
      .then((response) => response.json())
      .then((data) => {
        setItems(data);
        console.log("ITEM DATA:", data);
      })
      .catch((error) => console.error("Error:", error));
    }
  useEffect(() => {
      fetchItems();
  }, [update]);

  return items != null ? (
    <main>
      <RankingGrid
        items={items}
        imgArr={imgArr}
        drag={drag}
        allowDrop={allowDrop}
        drop={drop}
      />
      <ItemCollection items={items} drag={drag} imgArr={imgArr} />
      <button onClick={Reload} className="reload" style={{ marginTop: "10px" }}>
        {" "}
        <span className="text">Reload</span>{" "}
      </button>

      <div>
 
</div>
    </main>
  ) : (
    <main>Loading...</main>
  );
};
export default RankItems;
