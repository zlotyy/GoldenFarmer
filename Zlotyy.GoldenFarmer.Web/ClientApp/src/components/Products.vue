<template>
    <h1 id="tableLabel">Products</h1>    

    <p v-if="!products"><em>Loading...</em></p>

    <table class='table table-striped' aria-labelledby="tableLabel" v-if="products">
        <thead>
            <tr>
                <th>Id</th>
                <th>Produkt</th>
                <th>Czy dostêpny</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="product of products" v-bind:key="product">
                <td>{{ product.productId }}</td>
                <td>{{ product.name }}</td>
                <td>{{ product.isAvailable }}</td>
            </tr>
        </tbody>
    </table>
</template>


<script>
    import axios from 'axios'
    export default {
        name: "Products",
        data() {
            return {
                products: []
            }
        },
        methods: {
            getProducts() {
                axios.get('/products')
                    .then((response) => {
                        this.products = response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        },
        mounted() {
            this.getProducts();
        }
    }
</script>