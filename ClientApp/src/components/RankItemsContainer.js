import { useState, useEffect } from "react";
import RankItems from "./RankItems";

const RankItemsContainer = ({ dataType, imgArr }) => {
  const albumLocalStorageKey = "albums";
  const movieLocalStorageKey = "movies";

  var localStorageKey = "";

  const [albumItems, setAlbumItems] = useState(
    JSON.parse(localStorage.getItem(albumLocalStorageKey))
  );
  const [movieItems, setMovieItems] = useState(
    JSON.parse(localStorage.getItem(movieLocalStorageKey))
  );

  var data = [];
  var setFunc = null;

  
  const [itemData, setItemData] = useState(null);
  const [database, setDatabase] = useState(null);

  useEffect(() => {
    if (dataType === 1) {
      
      fetch("/api/MovieItem")
      .then((response) => response.json())
      .then((data) => {
        setItemData(data);
        console.log("ITEM DATA:", data);
      })
      .catch((error) => console.error("Error:", error));

      setDatabase("Movie")
      data = movieItems;
      setFunc = setMovieItems;
      localStorageKey = movieLocalStorageKey;
    } else if (dataType === 2) {

      fetch("/api/AlbumItem")
      .then((response) => response.json())
      .then((data) => {
        setItemData(data);
        console.log("ITEM DATA:", data);
      })
      .catch((error) => console.error("Error:", error));

      setDatabase("Album")
      data = albumItems;
      setFunc = setAlbumItems;
      localStorageKey = albumLocalStorageKey;
    }
    
  }, [dataType]);

  return (
    <RankItems
      items={itemData}
      setItems={setFunc}
      dataType={dataType}
      imgArr={imgArr}
      database={database}
    />
  );
};
export default RankItemsContainer;
