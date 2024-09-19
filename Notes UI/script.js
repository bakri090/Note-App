const SaveBtn = document.querySelector('#saveBtn');
const title = document.querySelector('#title');
const description = document.querySelector('#description');
const notesContainer = document.querySelector('#note_container');
const deleteBtn = document.querySelector('#btnDelete');

function clearForm(){
    title.value = '';
    description.value = '';
    deleteBtn.classList.add('hidden');
}

function displayNoteInForm(note){
    title.value = note.title;
    description.value = note.description;
    deleteBtn.classList.remove('hidden');
    deleteBtn.setAttribute('data-id',note.id)
    SaveBtn.setAttribute('data-id',note.id)
}

function getAllNodeById(id){
    fetch(`https://localhost:7205/api/notes/${id}`)
    .then(data => data.json())
    .then(response => displayNoteInForm(response))
}

function pupulate(id){
    getAllNodeById(id);
}   
function addNote(title,description){
    const body ={
        title: title,
        description:description,
        isVisible:true
    }
    fetch('https://localhost:7205/api/notes',{
        method:'POST',
        body: JSON.stringify(body),
        headers :{
            "content-type":"application/json"
        }
    }
    ).then(data => data.json())
    .then(Response => {
        clearForm();
        getAllNote();
    })
}

function displayNote(notes){
    let allNotes = '';
    notes.forEach(note => {
        const noteElement = `
                             <div class="note" data-id="${note.id}">
                             <h3>${note.title} </h3>
                             <p>${note.description} <p>
                             </div>
        `;
        allNotes += noteElement;
    });
    notesContainer.innerHTML =allNotes;

    //add eventlistener 
    document.querySelectorAll('.note').forEach(note => {
        note.addEventListener('click',function (){
            pupulate(note.dataset.id);
        });
    });
}

function getAllNote(){
    fetch('https://localhost:7205/api/notes')
    .then(data => data.json())
    .then(Response => displayNote(Response))
};

getAllNote();

function updateNote(id,title,description){
    const body ={
        title: title,
        description:description,
        isVisible:true
    };

    fetch(`https://localhost:7205/api/notes/${id}`,{
        method:'PUT',
        body: JSON.stringify(body),
        headers :{
            "content-type":"application/json"
        }
    }
    ).then(data => data.json())
    .then(Response => {
        clearForm();
        getAllNote();
    });
}

SaveBtn.addEventListener('click', function (){

    const id =SaveBtn.dataset.id;
    if(id){
        updateNote(id,title.value,description.value)
    }else{

        addNote(title.value,description.value)
    }
})

function deleteNote(id)
{
    fetch(`https://localhost:7205/api/notes/${id}`,{
        method:'DELETE',
        headers :{
            "content-type":"application/json"
        }
    }
    )
    .then(Response => {
        console.log(Response)
        clearForm();
        getAllNote();
    })
}
deleteBtn.addEventListener('click',function (){
    const id = deleteBtn.dataset.id;

    deleteNote(id);

})