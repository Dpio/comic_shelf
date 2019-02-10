export class MenuItem {
    name = '';
    icon = '';
    route = '';
    items: MenuItem[];

    constructor(name: string, icon: string, route: string, childItems: MenuItem[] = null) {
        this.name = name;
        this.icon = icon;
        this.route = route;
        this.items = childItems;
    }
}
