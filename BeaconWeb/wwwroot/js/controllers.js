/**
 * INSPINIA - Responsive Admin Theme
 * Copyright 2015 Webapplayers.com
 *
 */

/**
 * MainCtrl - controller
 */
function MainCtrl() {

    this.userName = 'Roman Kagan';
    this.helloText = 'Welcome in Treasure Hunter';
    this.descriptionText = 'Here we will watch for the pirates !';

    var scope = this;

    this.usersGridOptions = {
        data: 'main.users',
        enableRowSelection: false,
        multiSelect: false,
        enableColumnResize: true,
        showFilter: false,
        showGroupPanel: true,
        columnDefs: [
        { field: 'Name', displayName: 'Name' },
        { field: 'Position', displayName: 'Position' },
        { field: 'Count', displayName: 'Count' },
        { field: 'Status', displayName: 'Status' },
        { field: 'LastSeen', displayName: 'LastSeen' },
        ],
    }
};


angular
    .module('inspinia')
    .controller('MainCtrl', MainCtrl)