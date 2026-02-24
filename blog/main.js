import { marked } from "https://cdn.jsdelivr.net/npm/marked/lib/marked.esm.js";

const postPicker = document.getElementById("post-picker");
const postContent = document.getElementById("post-content");
const pathPrefix = "./posts";

function fetch_json_data(){
    fetch("./posts/posts.json", {method: 'GET', headers: {'Accept': 'application/json'}})
        .then(res => res.json())
        .then(res => {
            res.posts.forEach(post => {
                const newLink = document.createElement('button')
                const postTitle = document.createTextNode(post.title)
                newLink.append(postTitle)
                newLink.addEventListener('click', function(){fetch_md_file(post.filename)})
                newLink.className = "post-picker-button"
                postPicker.appendChild(newLink)
            });
        });
}

function fetch_md_file(filename){
    window.location.hash = filename;
    const filepath = pathPrefix + "/" + filename;
    fetch(filepath, {method: 'GET', headers: {'Accept': 'text'}})
        .then(res => res.text())
        .then(res => {
            var postText = marked.parse(res)
            postContent.innerHTML = postText;
        });
}

fetch_json_data();

let wHash = window.location.hash.split('#')[1]
if (wHash){
    fetch_md_file(wHash);
}

window.addEventListener('hashchange', function(){fetch_md_file(window.location.hash.split('#')[1])})