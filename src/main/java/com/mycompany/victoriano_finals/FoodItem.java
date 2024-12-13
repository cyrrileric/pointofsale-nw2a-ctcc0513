/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package com.mycompany.victoriano_finals;

/**
 *
 * @author cyrri
 */
public class FoodItem extends MenuItem {
    public FoodItem(int id, String name, double price, String imagePath) {
        super(id, name, price, imagePath);
    }

    @Override
    public double calculateTotal() {
        return price * quantity;
    }
}