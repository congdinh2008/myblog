document.getElementById('Name').addEventListener("input", function () {
    let title = document.getElementById('Name').value;
    console.log(title);
    title = title.toLowerCase();
    title = title.replace(/\W+|_+/g, '-').replace(/-{2,}/g, '-').replace(/^[-]+/g, '');
    document.getElementById('Slug').value = title;
    console.log(document.getElementById('Slug').value);
});