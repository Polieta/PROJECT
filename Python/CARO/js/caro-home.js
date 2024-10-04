function handleLetGo() {
    let typePlay = document.getElementById("list-type-play").value;
    let rows = document.getElementById("list-row").value;
    let columns = document.getElementById("list-column").value;
  
    if (typePlay === "" || rows === "" || columns === "") {
      alert("Vui lòng chọn kiểu chơi");
      return;
    }
    window.location.href =
      "C:/NQC/HK1(Nam3)(2023-2024)/Trí tuệ nhân tạo/caro_1/caro.html?type=" +
      typePlay +
      "&rows=" +
      rows +
      "&columns=" +
      columns;
  }
  