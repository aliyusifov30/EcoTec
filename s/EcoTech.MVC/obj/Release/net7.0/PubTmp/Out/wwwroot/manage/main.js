

let getFiles = document.querySelectorAll(".file");
console.log(getFiles)

getFiles.forEach(element => {

    var fileContent = element.nextElementSibling.nextElementSibling;

    console.log(fileContent)


    element.onchange = function (e) {

        let files = e.target.files
        let filesarr = [...files]
        filesarr.forEach(x => {
            if (x.type.startsWith("image/")) {
                let reader = new FileReader()
                reader.onload = function () {
                    let newimg = document.createElement("img")
                    newimg.style.width = "190px"
                    newimg.setAttribute("src", reader.result)

                    fileContent.innerHTML = "";
                    fileContent.appendChild(newimg)
                }
                reader.readAsDataURL(x)
            }
        })

    }
});