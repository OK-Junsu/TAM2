const uri = "api/Attendances";

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'attendance' : 'attendances';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function displayItems(data) {
    const tBody = document.getElementById('todos');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let detailsButton = button.cloneNode(false);
        detailsButton.innerText = 'Details';
        detailsButton.setAttribute('onclick', `moveToDetailsPage(${item.id})`);

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `moveToEditPage(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode_name = document.createTextNode(item.name);
        td1.appendChild(textNode_name);

        let td2 = tr.insertCell(1);
        let textNode_arrive = document.createTextNode(item.arrive);
        td2.appendChild(textNode_arrive);

        let td3 = tr.insertCell(2);
        let textNode_leave = document.createTextNode(item.leave);
        td3.appendChild(textNode_leave);

        let td4 = tr.insertCell(3);
        td4.appendChild(detailsButton);

        let td5 = tr.insertCell(4);
        td5.appendChild(editButton);

        let td6 = tr.insertCell(5);
        td6.appendChild(deleteButton);
    });
}

function moveToEditPage(id) {
    window.location.href = `edit.html?id=${id}`;
}

function moveToEditPageAtDetail() {
    const id = document.getElementById('detail-id').value;
    window.location.href = `edit.html?id=${id}`;
}

function getParameter(name) {
    var rtnval = '';
    var nowAddress = unescape(location.href);
    var parameters = (nowAddress.slice(nowAddress.indexOf('?') + 1,
        nowAddress.length)).split('&');
    for (var i = 0; i < parameters.length; i++) {
        var varName = parameters[i].split('=')[0];
        if (varName.toUpperCase() == name.toUpperCase()) {
            rtnval = parameters[i].split('=')[1];
            break;
        }
    }
    return rtnval;
}

function getEditData(id) {
    fetch(`${uri}/${id}`)
        .then(response => response.json())
        .then(data => displayEditForm(data));
}

function displayEditForm(data) {
    const item = data;

    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-arrive').value = item.arrive;
    document.getElementById('edit-leave').value = item.leave;
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        name: document.getElementById('edit-name').value.trim(),
        arrive: document.getElementById('edit-arrive').value.trim(),
        leave: document.getElementById('edit-leave').value.trim()
    };

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .catch(error => console.error('Unable to update item.', error));

    moveToIndexPage();

    return false;
}

function moveToIndexPage() {
    window.location.href = `index.html`;
}

function moveToDetailsPage(id) {
    window.location.href = `details.html?id=${id}`;
}

function getDetailData(id) {
    fetch(`${uri}/${id}`)
        .then(response => response.json())
        .then(data => displayDetailsForm(data));
}

function displayDetailsForm(data) {
    const item = data;

    document.getElementById('detail-id').value = item.id;
    document.getElementById('detail-name').value = item.name;
    document.getElementById('detail-arrive').value = item.arrive;
    document.getElementById('detail-leave').value = item.leave;
}

function moveToAddPage() {
    window.location.href = `add.html`;
}

function addItem() {
    const item = {
        name: document.getElementById('add-name').value.trim(),
        arrive: document.getElementById('add-arrive').value.trim(),
        leave: document.getElementById('add-leave').value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .catch(error => console.error('Unable to add item.', error));

    moveToIndexPage();
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}