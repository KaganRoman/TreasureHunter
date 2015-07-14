/**
 * INSPINIA - Responsive Admin Theme
 * Copyright 2015 Webapplayers.com
 *
 */

/**
 * MainCtrl - controller
 */
function MainCtrl($scope, $http, $interval, Hub) {

    this.userName = 'Roman Kagan';
    this.helloText = 'Welcome in Treasure Hunter';
    this.descriptionText = 'Here we will watch for the pirates !';

    var scope = this;
    this.users = [];

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

    var hub = new Hub('beaconHub', {
        methods: ['updateUsers'],
        listeners: {
            'updateServer': function (name, message) {
            },
        }
    });

    $interval(function() {
        hub.updateUsers("update", "users");
    }, 5000);
};


angular
    .module('inspinia')
    .controller('MainCtrl', MainCtrl)