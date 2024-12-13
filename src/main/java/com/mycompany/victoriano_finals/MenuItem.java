/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package com.mycompany.victoriano_finals;

import javax.swing.ImageIcon;

/**
 *
 * @author cyrri
 */
public abstract class MenuItem {
    protected int id;
    protected String name;
    protected double price;
    protected int quantity;
    protected ImageIcon image;

    public MenuItem(int id, String name, double price, String imagePath) {
        this.id = id;
        this.name = name;
        this.price = price;
        this.quantity = 0;
        this.image = new ImageIcon(getClass().getResource(imagePath));
    }

    public abstract double calculateTotal();
    
    public ImageIcon getImage() { return image; }
    public int getId() { return id; }
    public String getName() { return name; }
    public double getPrice() { return price; }
    public int getQuantity() { return quantity; }
    public void incrementQuantity() { quantity++; }
    public void setQuantity(int quantity) { this.quantity = quantity; }
}