function showPassword(dom_on, dom_off, field) {
  dom_on.addEventListener("click", () => {
    dom_on.style.display = "none";
    dom_off.style.display = "block";
    field.type = "text";
  });
  dom_off.addEventListener("click", () => {
    dom_off.style.display = "none";
    dom_on.style.display = "block";
    field.type = "password";
  });
}

export default showPassword;
