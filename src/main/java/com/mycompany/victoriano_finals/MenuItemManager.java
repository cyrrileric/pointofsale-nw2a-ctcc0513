/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package com.mycompany.victoriano_finals;

import java.util.HashMap;
import java.util.Map;

/**
 *
 * @author cyrri
 */
public class MenuItemManager {
    private Map<Integer, MenuItem> menuItems;
    
    public MenuItemManager() {
        menuItems = new HashMap<>();
        initializeMenuItems();
    }
    
    private void initializeMenuItems() {
        // Assuming your images are in src/main/resources/images/
        menuItems.put(1, new FoodItem(1, "Quarter/2Pc (HC)", 1060.50, "/images/quarter.png"));
        menuItems.put(2, new FoodItem(2, "Half /4Pc (HC)", 1990.99, "/images/half.png"));
        menuItems.put(3, new FoodItem(3, "BUCKET/6PC (H&C)", 2850.00, "/images/bucket6.png"));
        menuItems.put(4, new FoodItem(4, "Full/8Pc (HC)", 3880.00, "/images/full8.png"));
        menuItems.put(5, new FoodItem(5, "Bucket/12 Pc (HC)", 5650.00, "/images/bucket12.png"));
        menuItems.put(6, new FoodItem(6, "1 Pc Choice (HC)", 680.00, "/images/single.png"));
    }
    
    public MenuItem getMenuItem(int id) {
        return menuItems.get(id);
    }
}