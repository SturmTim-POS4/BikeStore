/**
 * BikeStore
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import * as models from './models';

export interface ProductDto {
    productId: number;

    productName: string;

    brandId: number;

    categoryId: number;

    modelYear?: number;

    listPrice: number;

    brandName: string;

    categoryName: string;

}
