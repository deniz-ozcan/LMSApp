let scs = document.getElementsByClassName("alertsuccess")[0];
    let dng = document.getElementsByClassName("alertdanger")[0];
    let wrn = document.getElementsByClassName("alertwarning")[0];
    let div = document.getElementsByClassName("content")[0];
    let userbody = document.getElementById("userbody");
    let rolebody = document.getElementById("rolebody");
    let productbody = document.getElementById("productbody");
    if (scs) {
      scs.addEventListener("animationend", () => {
        scs.remove();
      });
    }
    if (dng) {
      dng.addEventListener("animationend", () => {
        dng.remove();
      });
    }
    let count = 0;
    if (wrn) {
      wrn.addEventListener("animationend", () => {
        if (count == 0) {
            if (wrn.innerText == "No User") {
            userbody.insertAdjacentHTML("beforeend",
              `<div class="row mt-3">
                <div class="col-3 mx-auto">
                  <div class="alert alert-warning text-center">
                    ${wrn.innerText}
                  </div>
                </div>
              </div>`
            );
          } else if (wrn.innerText == "No Product") {
            productbody.insertAdjacentHTML("beforeend",
              `<div class="row mt-3">
                <div class="col-3 mx-auto">
                  <div class="alert alert-warning text-center">
                    ${wrn.innerText}
                  </div>
                </div>
              </div>`
            );
          } else {
            div.insertAdjacentHTML("beforeend",
              `<div class="row mt-3">
                  <div class="col-3 mx-auto">
                    <div class="alert alert-warning text-center">
                      ${wrn.innerText}
                    </div>
                  </div>
                </div>`
            );
          }
        }
        count++;
        wrn.remove();
      });
    }