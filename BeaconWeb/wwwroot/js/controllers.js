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
    this.beacons = [];

    var dateTemplate = '<div class="timeCell"><span>{{COL_FIELD | date:"HH:mm:ss"}}</span></div>';

    this.usersGridOptions = {
        data: 'main.users',
        enableRowSelection: false,
        multiSelect: false,
        enableColumnResize: true,
        showFilter: false,
        showGroupPanel: true,
        columnDefs: [
        { field: 'Id', displayName: 'Id' },
        { field: 'Name', displayName: 'Name' },
        { field: 'Position', displayName: 'Position' },
        { field: 'Count', displayName: 'Count' },
        { field: 'Around', displayName: 'Near' },
        { field: 'Status', displayName: 'Status' },
        { field: 'Timer', displayName: 'Time' },
        { field: 'LastSeen', displayName: 'LastSeen', cellTemplate: dateTemplate },
        ],
    }

    this.beaconsGridOptions = {
            data: 'main.beacons',
            enableRowSelection: false,
            multiSelect: false,
            enableColumnResize: true,
            showFilter: false,
            showGroupPanel: false,
            columnDefs: [
            { field: 'Name', displayName: 'Name' },
            { field: 'Visited', displayName: '# Found' },
            { field: 'Visible', displayName: 'Visible Now' },
        ],
    }

    var countVisitedBeacons = function(u, visited) {
        if (!u.Beacons) return 0;
        return _.reduce(u.Beacons, function(memo, beacon) {
            var num = 0;
            if (visited && beacon.Visited === true)
                num = 1;
            if (!visited && beacon.ProximityString && beacon.ProximityString !== 'unknown')
                num = 1;
            return memo + num;
        }, 0);
    };

    var hub = new Hub('beaconHub2', {
        methods: ['updateUsersPosition'],
        listeners: {
            'updateServer': function (message) {
                var users = scope.users;
                var user = _.find(users, function (u) { return u.Id === message.UserId });
                if (user == null) {
                    user = { Id: message.UserId };
                    users.push(user);
                }
                user.Name = message.Name;
                user.Status = message.Status;
                user.Timer = message.Timer;
                user.Beacons = message.Beacons;
                user.LastSeen = Date.now();
                user.Count = countVisitedBeacons(user, true);
                user.Around = countVisitedBeacons(user, false);
                var sortedUsers = _.sortBy(users, function (u) { return u.Count; });
                _.each(users, function(u) {
                    u.Position = sortedUsers.length - _.indexOf(sortedUsers, u);
                });

                var beacons = [];
                _.each(users, function (u) {
                    if (!u.Beacons) return;
                    _.each(u.Beacons, function(b) {
                        var beacon = _.find(beacons, function (i) { return i.Name === b.Name });
                        if (beacon == null) {
                            beacon = { Name: b.Name, Visited: 0, Visible: 0 };
                            beacons.push(beacon);
                        }
                        if (b.Visited === true) beacon.Visited++;
                        if (b.ProximityString && b.ProximityString !== 'unknown')
                            beacon.Visible++;
                    });
                });
                scope.beacons = beacons;
            },
        }
    });

    hub.promise.done(function() {
        $interval(function () {
            var positions = {};
            positions.length = 0;
            _.each(scope.users, function(user) {
                positions[user.Id] = user.Position;
                positions.length++;
            });
            hub.updateUsersPosition(positions);
        }, 5000);
    });
};


angular
    .module('inspinia')
    .controller('MainCtrl', MainCtrl)